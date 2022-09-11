namespace DependencyManagement.Injection.Builders;

using Composition.Components;
using Composition.Containers;
using Composition.Extensions;
using Composition.Utils;
using Extensions;
using Providers;
using Strategies;
using Targets;

internal sealed class AddableTargetBuilder<T> : ITargetBuilder<T> where T : class
{
    private readonly List<Action<ILazyComponent<IProvider>, ILazyComponent<IStrategy>>> _abstractions = new();
    private readonly IContainer _currentContainer;
    private readonly IContainer _rootContainer;

    private Func<ILazyComponent<IProvider>> _provider;

    private Func<ILazyComponent<IStrategy>> _strategy;

    public AddableTargetBuilder(IContainer container)
    {
        _currentContainer = container;

        _rootContainer = TraversalService.GetInitial(container);

        _strategy = () => _rootContainer.LastLazy<SingletonStrategy>();
        _provider = () => _rootContainer.LastLazy<IProvider<T>>();
    }

    public IAbstractionTargetBuilder<T> As<TAs>() where TAs : class
    {
        _abstractions.Add((provider, strategy) =>
        {
            var target = new Target<TAs>(strategy, provider);

            _currentContainer.TryAddTarget(target);
        });

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

        var provider = _provider();
        var strategy = _strategy();

        foreach (var abstraction in _abstractions)
        {
            abstraction(provider, strategy);
        }
    }
}