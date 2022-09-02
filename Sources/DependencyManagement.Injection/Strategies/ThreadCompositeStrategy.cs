namespace DependencyManagement.Injection.Strategies;

using System.Collections.Concurrent;
using Composition.Components;
using Composition.Composites;
using Core.Caches;
using Providers;

public sealed class ThreadCompositeStrategy : Strategy
{
    private readonly DisposableCollection _disposableCollection = new();

    private readonly
        ConcurrentDictionary<Thread, ConcurrentDictionary<IReadOnlyComposite, ConcurrentDictionary<IComponent, object>>>
        _instances = new();

    public override T GetInstance<T>(IReadOnlyComposite composite, IProvider<T> provider)
    {
        ArgumentNullException.ThrowIfNull(composite);
        ArgumentNullException.ThrowIfNull(provider);
        if (IsDisposed) throw new ObjectDisposedException(nameof(SingletonStrategy));

        if (!_instances.TryGetValue(Thread.CurrentThread, out var composites))
        {
            composites = new ConcurrentDictionary<IReadOnlyComposite, ConcurrentDictionary<IComponent, object>>();
            if (!_instances.TryAdd(Thread.CurrentThread, composites)) return GetInstance(composite, provider);
        }

        if (!composites.TryGetValue(composite, out var instances))
        {
            instances = new ConcurrentDictionary<IComponent, object>();
            if (!composites.TryAdd(composite, instances)) return GetInstance(composite, provider);
        }

        if (!instances.TryGetValue(provider, out var instance))
        {
            instance = provider.GetInstance(composite);
            if (instance is null) throw new NullReferenceException(nameof(instance));

            _disposableCollection.Add(instance);

            if (!instances.TryAdd(provider, instance)) return GetInstance(composite, provider);
        }

        composite.OnDisposing += CompositeOnOnDisposing;

        return (T)instance;
    }

    private void CompositeOnOnDisposing(object? sender)
    {
        var composite = (IReadOnlyComposite)sender!;

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