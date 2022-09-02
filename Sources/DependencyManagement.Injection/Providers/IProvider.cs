namespace DependencyManagement.Injection.Providers;

using Composition.Components;
using Composition.Containers;

public interface IProvider<out T> : IComponent where T : notnull
{
    T GetInstance(IReadOnlyContainer container);
}