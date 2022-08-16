namespace DependencyManagement.Composition.Components;

public interface ILazyComponent<out T> : IComponent where T : class, IComponent
{
    T Value { get; }

    bool IsValueCreated { get; }
}