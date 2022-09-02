namespace DependencyManagement.Injection.Strategies;

using System.Collections.Concurrent;
using Composition.Components;
using Composition.Containers;
using Core.Caches;
using Providers;

public sealed class SingletonStrategy : Strategy
{
    private readonly DisposableCollection _disposableCollection = new();
    private readonly ConcurrentDictionary<IComponent, object> _instances = new();

    public override T GetInstance<T>(IReadOnlyContainer container, IProvider<T> provider)
    {
        ArgumentNullException.ThrowIfNull(container);
        ArgumentNullException.ThrowIfNull(provider);
        if (IsDisposed) throw new ObjectDisposedException(nameof(SingletonStrategy));

        if (!_instances.TryGetValue(provider, out var instance))
        {
            instance = provider.GetInstance(container);
            if (instance is null) throw new NullReferenceException(nameof(instance));

            _disposableCollection.Add(instance);

            if (!_instances.TryAdd(provider, instance)) return GetInstance(container, provider);
        }

        return (T)instance;
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