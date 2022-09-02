namespace DependencyManagement.Injection.Targets;

using Composition.Components;
using Composition.Composites;
using Providers;
using Strategies;

internal sealed class Target<T> : Component, ITarget<T> where T : notnull
{
    private readonly ILazyComponent<IProvider<T>> _provider;
    private readonly ILazyComponent<IStrategy> _strategy;

    public Target(ILazyComponent<IStrategy> strategy, ILazyComponent<IProvider<T>> provider)
    {
        _strategy = strategy;
        _provider = provider;
    }

    public T GetInstance(IReadOnlyComposite composite)
    {
        return _strategy.Value.GetInstance(composite, _provider.Value);
    }
}