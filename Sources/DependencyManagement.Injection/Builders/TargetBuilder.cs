namespace DependencyManagement.Injection.Builders;

using Composition.Components;
using Composition.Containers;
using Composition.Extensions;
using Composition.Utils;
using Providers;
using Strategies;
using Targets;

internal sealed class TargetBuilder<T> : ITargetBuilder<T> where T : class
{
    private readonly List<Action<ITarget<T>>> _abstractions = new();
    private readonly IContainer _currentContainer;
    private readonly IContainer _rootContainer;

    private Func<ILazyComponent<IProvider<T>>> _provider;

    private Func<ILazyComponent<IStrategy>> _strategy;

    public TargetBuilder(IContainer container)
    {
        _currentContainer = container;

        _rootContainer = ContainerTreeUtils.GetLast(container);

        _strategy = () => _rootContainer.LastLazy<SingletonStrategy>();
        _provider = () => _rootContainer.LastLazy<IProvider<T>>();
    }

    public IAbstractionTargetBuilder<T> As<TAs>() where TAs : class
    {
        _abstractions.Add(target => _currentContainer.TryAdd((ITarget<TAs>)target));

        return this;
    }

    public IStrategyTargetBuilder<T> With(Func<IProvider<T>> provider)
    {
        _provider = () => new LazyComponent<IProvider<T>>(provider);

        return this;
    }

    public void To<TStrategy>() where TStrategy : class, IStrategy
    {
        _strategy = () => _rootContainer.LastLazy<TStrategy>();

        if (_abstractions.Count == 0) As<T>();

        var target = new Target<T>(_strategy(), _provider());

        foreach (var abstraction in _abstractions)
        {
            abstraction(target);
        }
    }
}