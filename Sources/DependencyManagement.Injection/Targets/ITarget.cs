namespace DependencyManagement.Injection.Targets;

using Composition.Components;
using Composition.Containers;

public interface ITarget<T> : IComponent where T : notnull
{
    T ProvideInstance(IReadOnlyContainer container);

    bool IsInstanceCached(T instance);

    void ResolveInstance(T instance);
}