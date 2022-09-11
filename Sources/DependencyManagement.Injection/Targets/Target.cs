namespace DependencyManagement.Injection.Targets;

using Composition.Components;
using Composition.Containers;
using Models;
using Providers;
using Strategies;

internal sealed class Target<T> : Component, ITarget<T> where T : notnull
{
    private readonly ILazyComponent<IProvider> _provider;
    
    private readonly ILazyComponent<IStrategy> _strategy;

    public Target(ILazyComponent<IStrategy> strategy, ILazyComponent<IProvider> provider)
    {
        _strategy = strategy;
        _provider = provider;
    }

    public T ProvideInstance(IReadOnlyContainer container)
    {
        var context = new StrategyContext<T>(container, _provider.Value);
        
        return _strategy.Value.GetInstance(context);
    }

    public bool IsInstanceCached(T instance)
    {
        return _strategy.Value.ContainsInstance(instance);
    }

    public void ResolveInstance(T instance)
    {
        _strategy.Value.RemoveInstance(instance);
    }
}