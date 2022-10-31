namespace DependencyManagement.Targets;

using Components;
using Containers;

public interface ITarget<T> : IComponent where T : notnull
{
    T GetInstance(IReadOnlyContainer container);
    
    bool ContainsInstance(T instance);

    bool RemoveInstance(T instance);
}
