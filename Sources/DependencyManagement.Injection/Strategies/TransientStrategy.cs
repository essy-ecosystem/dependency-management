namespace DependencyManagement.Injection.Strategies;

using System.Collections.Concurrent;
using Composition.Containers;
using Core.Caches;
using Core.Utils;
using Models;

public sealed class TransientStrategy : Strategy
{
    private readonly ConcurrentDictionary<IReadOnlyContainer, IDisposableCollection> _disposablesCollections;
    
    /// <summary/>
    public TransientStrategy()
    {
        _disposablesCollections = new();
    }

    /// <inheritdoc />
    public override T GetInstance<T>(StrategyContext<T> context)
    {
        Thrower.ThrowIfObjectDisposed(this);
        
        Thrower.ThrowIfArgumentNull(context.Container);
        Thrower.ThrowIfObjectDisposed(context.Container);
        
        Thrower.ThrowIfArgumentNull(context.Provider);
        Thrower.ThrowIfObjectDisposed(context.Provider);
        
        var instance = context.Provider.CreateInstance(context.Container);
        
        Thrower.ThrowIfObjectNull(instance);

        AddInstanceToDisposableCollection(context.Container, instance);

        return (T)instance;
    }

    /// <inheritdoc />
    public override bool ContainsInstance<T>(T instance)
    {
        Thrower.ThrowIfArgumentNull(instance);

        return _disposablesCollections.Values
            .Any(collection => collection.Contains(instance));
    }
    
    /// <inheritdoc />
    public override bool RemoveInstance<T>(T instance)
    {
        Thrower.ThrowIfArgumentNull(instance);
        
        var pair = _disposablesCollections
            .FirstOrDefault(pair => pair.Value.Contains(instance));
        
        if (pair.Equals(default)) return false;
        
        DisposeInstance(instance);
        return pair.Value.Remove(instance);
    }

    private void AddInstanceToDisposableCollection(IReadOnlyContainer container, object instance)
    {
        if (_disposablesCollections.TryGetValue(container, out var disposableCollection))
        {
            disposableCollection.Add(instance);
            return;
        }

        container.Disposing += OnContainerDisposing;

        var initialDisposableCollection = new DisposableCollection(instance);
        if (_disposablesCollections.TryAdd(container, initialDisposableCollection)) return;
        
        AddInstanceToDisposableCollection(container, instance);
    }

    private void OnContainerDisposing(object sender)
    {
        if (IsDisposed) return;

        if (!_disposablesCollections.TryRemove((IReadOnlyContainer)sender, out var disposableCollection)) return;
        
        disposableCollection.Dispose();
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

    /// <inheritdoc />
    protected override void DisposeCore(bool disposing)
    {
        if (disposing)
        {
            foreach (var disposableCollection in _disposablesCollections.Values)
            {
                disposableCollection.Dispose();
            }
        }
        
        _disposablesCollections.Clear();
        
        base.DisposeCore(disposing);
    }

    /// <inheritdoc />
    protected override async ValueTask DisposeCoreAsync()
    {
        foreach (var disposableCollection in _disposablesCollections.Values)
        {
            await disposableCollection.DisposeAsync();
        }
        
        await base.DisposeCoreAsync();
    }
}