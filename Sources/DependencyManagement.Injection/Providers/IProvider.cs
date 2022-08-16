using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;

namespace DependencyManagement.Injection.Providers;

public interface IProvider<out T> : IComponent where T : notnull
{
    T GetInstance(IReadOnlyComposite composite);
}