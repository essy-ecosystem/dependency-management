namespace DependencyManagement.Injection.Strategies;

using System.Collections.Concurrent;
using Composition.Components;
using Composition.Containers;
using Core.Caches;
using Providers;

public sealed class ThreadCompositeStrategy : Strategy
{
    private readonly DisposableCollection _disposableCollection = new();

    private readonly
        ConcurrentDictionary<Thread, ConcurrentDictionary<IReadOnlyContainer, ConcurrentDictionary<IComponent, object>>>
        _instances = new();

    public override T GetInstance<T>(IReadOnlyContainer container, IProvider<T> provider)
    {
        ArgumentNullException.ThrowIfNull(container);
        ArgumentNullException.ThrowIfNull(provider);
        if (IsDisposed) throw new ObjectDisposedException(nameof(SingletonStrategy));

        if (!_instances.TryGetValue(Thread.CurrentThread, out var composites))
        {
            composites = new ConcurrentDictionary<IReadOnlyContainer, ConcurrentDictionary<IComponent, object>>();
            if (!_instances.TryAdd(Thread.CurrentThread, composites)) return GetInstance(container, provider);
        }

        if (!composites.TryGetValue(container, out var instances))
        {
            instances = new ConcurrentDictionary<IComponent, object>();
            if (!composites.TryAdd(container, instances)) return GetInstance(container, provider);
        }

        if (!instances.TryGetValue(provider, out var instance))
        {
            instance = provider.GetInstance(container);
            if (instance is null) throw new NullReferenceException(nameof(instance));

            _disposableCollection.Add(instance);

            if (!instances.TryAdd(provider, instance)) return GetInstance(container, provider);
        }

        container.OnDisposing += CompositeOnOnDisposing;

        return (T)instance;
    }

    private void CompositeOnOnDisposing(object? sender)
    {
        var composite = (IReadOnlyContainer)sender!;

        foreach (var composites in _instances.Values)
        {
            if (!composites.TryRemove(composite, out var instances)) continue;

            foreach (var instance in instances.Values)
            {
                if (instance is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                else if (instance is IAsyncDisposable asyncDisposable) asyncDisposable.DisposeAsync().AsTask().Wait();
            }
        }
    }

    protected override void DisposeCore(bool disposing)
    {
        if (disposing) _disposableCollection.Dispose();

        base.DisposeCore(disposing);
    }

    protected override async ValueTask DisposeCoreAsync()
    {
        await _disposableCollection.DisposeAsync();

        await base.DisposeCoreAsync();
    }
}