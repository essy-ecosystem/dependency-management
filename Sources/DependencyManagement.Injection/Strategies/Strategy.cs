namespace DependencyManagement.Injection.Strategies;

using Composition.Components;
using Composition.Containers;
using Models;
using Providers;

public abstract class Strategy : Component, IStrategy
{
    public abstract T GetInstance<T>(StrategyContext<T> context) where T : notnull;

    public abstract bool ContainsInstance<T>(T instance) where T : notnull;

    public abstract bool RemoveInstance<T>(T instance) where T : notnull;
}