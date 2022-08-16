using System.Collections.Concurrent;
using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;
using DependencyManagement.Core.Caches;
using DependencyManagement.Injection.Providers;

namespace DependencyManagement.Injection.Strategies;

public sealed class SingletonStrategy : Strategy
{
    private readonly DisposableCache _cache = new();
    private readonly ConcurrentDictionary<IComponent, object> _instances = new();

    public override T GetInstance<T>(IReadOnlyComposite composite, IProvider<T> provider)
    {
        ArgumentNullException.ThrowIfNull(composite);
        ArgumentNullException.ThrowIfNull(provider);
        if (IsDisposed) throw new ObjectDisposedException(nameof(SingletonStrategy));

        if (!_instances.TryGetValue(provider, out var instance))
        {
            instance = provider.GetInstance(composite);
            if (instance is null) throw new NullReferenceException(nameof(instance));

            _cache.TryAdd(instance);

            if (!_instances.TryAdd(provider, instance)) return GetInstance(composite, provider);
        }

        return (T)instance;
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