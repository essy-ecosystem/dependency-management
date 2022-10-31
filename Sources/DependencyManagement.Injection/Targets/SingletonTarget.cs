namespace DependencyManagement.Targets;

using System.Runtime.CompilerServices;
using Components;
using Containers;
using Providers;

public sealed class SingletonTarget<T> : Target<T> where T : notnull
{
    private readonly object _lock = new();
    
    private readonly IProvider<T> _provider;
    
    private HashSet<T> _disposables = new();

    private bool _isAsyncDisposable;

    private bool? _isSupportDisposable;

    private T? _instance;
    
    public SingletonTarget(IProvider<T> provider)
    {
        _provider = provider;
    }

    public override T GetInstance(IReadOnlyContainer container)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);

        if (_instance is null)
        {
            lock (_lock)
            {
                if (_instance is not null) return _instance;

                var instance = _provider.CreateInstance(container);
            
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

                if (_isSupportDisposable == true) _disposables.Add(instance);

                _instance = instance;
            }
        }

        return _instance;
    }

    public override bool ContainsInstance(T instance)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        
        return _instance?.Equals(instance) == true;
    }

    public override bool RemoveInstance(T instance)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        
        lock (_lock)
        {
            if (ContainsInstance(instance) is false) return false;

            if (_instance is IDisposable or IAsyncDisposable) _disposables.Add(_instance);
            
            _instance = default;
        }

        return true;
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