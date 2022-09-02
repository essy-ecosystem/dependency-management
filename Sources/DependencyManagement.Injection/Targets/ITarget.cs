namespace DependencyManagement.Injection.Targets;

using Composition.Components;
using Composition.Composites;

public interface ITarget<out T> : IComponent where T : notnull
{
    T GetInstance(IReadOnlyComposite composite);
}