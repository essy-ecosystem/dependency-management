namespace DependencyManagement.Injection.Providers;

using Composition.Components;
using Composition.Composites;

public interface IProvider<out T> : IComponent where T : notnull
{
    T GetInstance(IReadOnlyComposite composite);
}