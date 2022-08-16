using System.Collections.Concurrent;
using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;
using DependencyManagement.Core.Caches;
using DependencyManagement.Core.Utils;
using DependencyManagement.Injection.Providers;

namespace DependencyManagement.Injection.Strategies;

public sealed class CompositeStrategy : Strategy
{
    private readonly DisposableCache _cache = new();

    private readonly ConcurrentDictionary<IReadOnlyComposite, ConcurrentDictionary<IComponent, object>> _instances =
        new();

    public override T GetInstance<T>(IReadOnlyComposite composite, IProvider<T> provider)
    {
        ThrowUtils.ThrowIfNull(composite);
        ThrowUtils.ThrowIfNull(provider);
        ThrowUtils.ThrowIfDisposed(IsDisposed);

        if (!_instances.TryGetValue(composite, out var instances))
        {
            instances = new ConcurrentDictionary<IComponent, object>();
            if (!_instances.TryAdd(composite, instances)) return GetInstance(composite, provider);
        }

        if (!instances.TryGetValue(provider, out var instance))
        {
            instance = provider.GetInstance(composite);
            if (instance is null) throw new NullReferenceException(nameof(instance));

            _cache.TryAdd(instance);

            if (!instances.TryAdd(provider, instance)) return GetInstance(composite, provider);
        }

        composite.OnDisposing += CompositeOnOnDisposing;

        return (T)instance;
    }

    private void CompositeOnOnDisposing(object? sender, bool e)
    {
        var composite = (IReadOnlyComposite)sender!;

        if (!_instances.TryRemove(composite, out var instances)) return;

        foreach (var instance in instances.Values)
            if (instance is IDisposable disposable) disposable.Dispose();
            else if (instance is IAsyncDisposable asyncDisposable) asyncDisposable.DisposeAsync().AsTask().Wait();
    }

    protected override void DisposeCore(bool disposing)
    {
        if (disposing) _cache.Dispose();

        base.DisposeCore(disposing);
    }

    protected override async ValueTask DisposeCoreAsync()
    {
        await _cache.DisposeAsync();

        await base.DisposeCoreAsync();
    }
}