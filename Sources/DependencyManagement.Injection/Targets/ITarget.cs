namespace DependencyManagement.Injection.Targets;

using Composition.Components;
using Composition.Containers;

public interface ITarget<out T> : IComponent where T : notnull
{
    T GetInstance(IReadOnlyContainer container);
}