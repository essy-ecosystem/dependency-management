namespace DependencyManagement.Injection.Strategies;

using Composition.Components;
using Composition.Containers;
using Providers;

public interface IStrategy : IComponent
{
    T GetInstance<T>(IReadOnlyContainer container, IProvider<T> provider) where T : notnull;
}