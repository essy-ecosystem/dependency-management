namespace DependencyManagement.Targets;

using System.Runtime.CompilerServices;
using Containers;
using Providers;

public sealed class TransientTarget<T> : Target<T> where T : notnull
{
    private readonly object _lock = new();
    
    private readonly IProvider<T> _provider;
    
    private HashSet<T> _disposables = new();

    private bool _isAsyncDisposable;

    private bool? _isSupportDisposable;

    public TransientTarget(IProvider<T> provider)
    {
        _provider = provider;
    }

    public override T GetInstance(IReadOnlyContainer container)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);

        var instance = _provider.CreateInstance(container);

        if (_isSupportDisposable is null)
        {
            lock (_lock)
            {
                if (_isSupportDisposable is null)
                {
                    switch (instance)
                    {
                        case IAsyncDisposable:
                            _isAsyncDisposable = true;
                            _isSupportDisposable = true;
                            break;
                        case IDisposable:
                            _isAsyncDisposable = false;
                            _isSupportDisposable = true;
                            break;
                        default:
                            _isSupportDisposable = false;
                            break;
                    }
                }
            }
        }

        if (_isSupportDisposable == true) _disposables.Add(instance);
        
        return instance;
    }

    public override bool ContainsInstance(T instance)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);

        return false;
    }

    public override bool RemoveInstance(T instance)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);

        return false;
    }
    
    protected override void DisposeCore(bool disposing)
    {
        if (disposing)
        {
            if (_isSupportDisposable == true)
            {
                if (_isAsyncDisposable)
                {
                    foreach (var currentDisposable in _disposables)
                    {
                        Unsafe.As<IAsyncDisposable>(currentDisposable).DisposeAsync().GetAwaiter().GetResult();
                    }
                }
                else
                {
                    foreach (var currentDisposable in _disposables)
                    {
                        Unsafe.As<IDisposable>(currentDisposable).Dispose();
                    }
                }
            }
        }
        
        _disposables = null!;
    }

    protected override async ValueTask DisposeCoreAsync()
    {
        if (_isSupportDisposable != true) return;
        
        if (_isAsyncDisposable)
        {
            foreach (var currentDisposable in _disposables)
            {
                await Unsafe.As<IAsyncDisposable>(currentDisposable).DisposeAsync();
            }
        }
        else
        {
            foreach (var currentDisposable in _disposables)
            {
                Unsafe.As<IDisposable>(currentDisposable).Dispose();
            }
        }
    }
}