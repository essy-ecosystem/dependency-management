namespace DependencyManagement.Injection.Models;

using Composition.Containers;
using Providers;

public readonly struct StrategyContext<T> where T : notnull
{
    public StrategyContext(IReadOnlyContainer container, IProvider provider)
    {
        Container = container;
        Provider = provider;
    }

    public IReadOnlyContainer Container { get; }

    public IProvider Provider { get; }
}