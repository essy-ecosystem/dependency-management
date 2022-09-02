namespace DependencyManagement.Injection.Strategies;

using Composition.Components;
using Composition.Composites;
using Providers;

public interface IStrategy : IComponent
{
    T GetInstance<T>(IReadOnlyComposite composite, IProvider<T> provider) where T : notnull;
}