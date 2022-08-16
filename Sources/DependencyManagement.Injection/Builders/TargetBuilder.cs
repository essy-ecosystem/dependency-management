using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;
using DependencyManagement.Composition.Extensions;
using DependencyManagement.Composition.Utils;
using DependencyManagement.Injection.Providers;
using DependencyManagement.Injection.Strategies;
using DependencyManagement.Injection.Targets;

namespace DependencyManagement.Injection.Builders;

internal sealed class TargetBuilder<T> : ITargetBuilder<T> where T : class
{
    private readonly List<Action<ITarget<T>>> _abstractions = new();
    private readonly IComposite _currentComposite;
    private readonly IComposite _rootComposite;

    private Func<ILazyComponent<IProvider<T>>> _provider;

    private Func<ILazyComponent<IStrategy>> _strategy;

    public TargetBuilder(IComposite composite)
    {
        _currentComposite = composite;

        _rootComposite = CompositeTreeUtils.GetLast(composite);

        _strategy = () => _rootComposite.LastLazy<SingletonStrategy>();
        _provider = () => _rootComposite.LastLazy<IProvider<T>>();
    }

    public IAbstractionTargetBuilder<T> As<TAs>() where TAs : class
    {
        _abstractions.Add(target => _currentComposite.TryAdd((ITarget<TAs>)target));

        return this;
    }

    public IStrategyTargetBuilder<T> With(Func<IProvider<T>> provider)
    {
        _provider = () => new LazyComponent<IProvider<T>>(provider);

        return this;
    }

    public void To<TStrategy>() where TStrategy : class, IStrategy
    {
        _strategy = () => _rootComposite.LastLazy<TStrategy>();

        if (_abstractions.Count == 0) As<T>();

        var target = new Target<T>(_strategy(), _provider());

        foreach (var abstraction in _abstractions) abstraction(target);
    }
}