namespace DependencyManagement.Injection.Strategies;

using System.Collections.Concurrent;
using Composition.Containers;
using Core.Utils;
using Models;

public sealed class ScopeStrategy : Strategy
{
    private readonly ConcurrentDictionary<IReadOnlyContainer, SingletonStrategy> _containersSingletonStrategies;

    /// <summary />
    public ScopeStrategy()
    {
        _containersSingletonStrategies = new ConcurrentDictionary<IReadOnlyContainer, SingletonStrategy>();
    }

    /// <inheritdoc />
    public override T GetInstance<T>(StrategyContext<T> context)
    {
        Thrower.ThrowIfObjectDisposed(this);

        if (_containersSingletonStrategies.TryGetValue(context.Container, out var currentSingletonStrategy))
        {
            return currentSingletonStrategy.GetInstance(context);
        }

        var singletonStrategy = new SingletonStrategy();

        context.Container.Disposing += OnContainerDisposing;

        return !_containersSingletonStrategies.TryAdd(context.Container, singletonStrategy)
            ? GetInstance(context)
            : singletonStrategy.GetInstance(context);
    }

    private void OnContainerDisposing(object sender)
    {
        if (IsDisposed) return;

        if (!_containersSingletonStrategies.TryRemove((IReadOnlyContainer)sender, out var singletonStrategy)) return;

        singletonStrategy.Dispose();
    }

    /// <inheritdoc />
    public override bool ContainsInstance<T>(T instance)
    {
        return _containersSingletonStrategies.Values.Any(singletonStrategy =>
            singletonStrategy.ContainsInstance(instance));
    }

    /// <inheritdoc />
    public override bool RemoveInstance<T>(T instance)
    {
        return _containersSingletonStrategies.Values.Any(singletonStrategy =>
            singletonStrategy.RemoveInstance(instance));
    }

    /// <inheritdoc />
    protected override void DisposeCore(bool disposing)
    {
        if (disposing)
        {
            foreach (var singletonStrategy in _containersSingletonStrategies.Values)
            {
                singletonStrategy.Dispose();
            }
        }

        _containersSingletonStrategies.Clear();

        base.DisposeCore(disposing);
    }

    /// <inheritdoc />
    protected override async ValueTask DisposeCoreAsync()
    {
        foreach (var singletonStrategy in _containersSingletonStrategies.Values)
        {
            await singletonStrategy.DisposeAsync();
        }

        await base.DisposeCoreAsync();
    }
}