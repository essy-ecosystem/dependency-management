namespace DependencyManagement.Injection.Strategies;

using Composition.Components;
using Models;

public interface IStrategy : IComponent
{
    T GetInstance<T>(StrategyContext<T> context) where T : notnull;

    bool ContainsInstance<T>(T instance) where T : notnull;

    bool RemoveInstance<T>(T instance) where T : notnull;
}