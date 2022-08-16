using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;

namespace DependencyManagement.Injection.Targets;

public interface ITarget<out T> : IComponent where T : notnull
{
    T GetInstance(IReadOnlyComposite composite);
}