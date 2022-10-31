namespace DependencyManagement.Targets;

using Components;
using Containers;

public abstract class Target<T> : Component, ITarget<T> where T : notnull
{
    public abstract T GetInstance(IReadOnlyContainer container);
    
    public abstract bool ContainsInstance(T instance);
    
    public abstract bool RemoveInstance(T instance);
}