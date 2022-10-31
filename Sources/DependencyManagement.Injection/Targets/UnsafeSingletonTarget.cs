namespace DependencyManagement.Targets;

using Containers;
using Providers;

public sealed class UnsafeSingletonTarget<T> : Target<T> where T : notnull
{
    private readonly object _locker = new();
    
    private readonly IProvider<T> _provider;

    private T? _instance;
    
    public UnsafeSingletonTarget(IProvider<T> provider)
    {
        _provider = provider;
    }

    public override T GetInstance(IReadOnlyContainer container)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);

        if (_instance is not null) return _instance;
        
        lock (_locker)
        {
            if (_instance is not null) return _instance;

            _instance = _provider.CreateInstance(container);
        }

        return _instance!;
    }

    public override bool ContainsInstance(T instance)
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);

        return _instance?.Equals(instance) ?? false;
    }

    public override bool RemoveInstance(T instance)
    {
        if (ContainsInstance(instance) is false) return false;
        
        lock (_locker)
        {
            if (ContainsInstance(instance) is false) return false;
            
            _instance = default;
            
            return true;
        }
    }
}