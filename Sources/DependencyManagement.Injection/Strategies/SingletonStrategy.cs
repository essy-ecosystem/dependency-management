namespace DependencyManagement.Injection.Strategies;

using System.Collections.Concurrent;
using Core.Disposables;
using Core.Utils;
using Models;
using Providers;

public sealed class SingletonStrategy : Strategy
{
    private readonly ConcurrentDictionary<IProvider, object> _providersInstances;

    /// <summary />
    public SingletonStrategy()
    {
        _providersInstances = new ConcurrentDictionary<IProvider, object>();
    }

    /// <inheritdoc />
    public override T GetInstance<T>(StrategyContext<T> context)
    {
        Thrower.ThrowIfObjectDisposed(this);

        Thrower.ThrowIfArgumentNull(context.Container);
        Thrower.ThrowIfObjectDisposed(context.Container);

        Thrower.ThrowIfArgumentNull(context.Provider);
        Thrower.ThrowIfObjectDisposed(context.Provider);

        if (_providersInstances.TryGetValue(context.Provider, out var cachedInstance))
        {
            return (T)cachedInstance;
        }

        var instance = context.Provider.CreateInstance(context.Container);

        Thrower.ThrowIfObjectNull(instance);

        AddInstanceToProvidersInstancesCache(context.Provider, instance);

        return (T)instance;
    }

    /// <inheritdoc />
    public override bool ContainsInstance<T>(T instance)
    {
        Thrower.ThrowIfArgumentNull(instance);

        return _providersInstances.Values
            .Contains(instance);
    }

    /// <inheritdoc />
    public override bool RemoveInstance<T>(T instance)
    {
        Thrower.ThrowIfArgumentNull(instance);

        var castedInstance = (object)instance;

        var pair = _providersInstances
            .FirstOrDefault(pair => pair.Value == castedInstance);

        return !pair.Equals(default) && RemoveProvider(pair.Key);
    }

    private void AddInstanceToProvidersInstancesCache(IProvider provider, object instance)
    {
        _providersInstances.TryAdd(provider, instance);
    }

    private bool RemoveProvider(IProvider provider)
    {
        if (!_providersInstances.TryRemove(provider, out var instance))
        {
            return false;
        }

        DisposeInstance(instance);
        return true;
    }

    private void DisposeInstance(object instance)
    {
        switch (instance)
        {
            case IDisposable disposableInstance:
                disposableInstance.Dispose();
                break;
            case IAsyncDisposable asyncDisposableInstance:
                asyncDisposableInstance.DisposeAsync().GetAwaiter().GetResult();
                break;
        }
    }

    private ValueTask DisposeInstanceAsync(object instance)
    {
        switch (instance)
        {
            case IAsyncDisposable asyncDisposableInstance:
                return asyncDisposableInstance.DisposeAsync();
            case IDisposable disposableInstance:
                disposableInstance.Dispose();
                break;
        }

        return default;
    }

    /// <inheritdoc />
    protected override void DisposeCore(bool disposing)
    {
        if (disposing)
        {
            foreach (var instance in _providersInstances.Values)
            {
                DisposeInstance(instance);
            }
        }

        _providersInstances.Clear();

        base.DisposeCore(disposing);
    }

    /// <inheritdoc />
    protected override async ValueTask DisposeCoreAsync()
    {
        foreach (var instance in _providersInstances.Values)
        {
            await DisposeInstanceAsync(instance);
        }

        await base.DisposeCoreAsync();
    }
}