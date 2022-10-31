namespace DependencyManagement.Containers;

using Components;
using Disposables;

public class Scope : AsyncDisposableObject, IScope
{
    public Scope(IReadOnlyContainer container)
    {
        Current = container is IScope scope 
            ? scope.Current 
            : container;
    }
    
    public IReadOnlyContainer Current { get; }

    public IReadOnlyContainer? Father => Current.Father;

    public IReadOnlyList<T> All<T>() where T : IComponent => Current.All<T>();

    public IReadOnlyList<T> All<T>(bool inherit) where T : IComponent
    {
        return Current.All<T>(inherit);
    }

    public bool Contains<T>(T component) where T : IComponent => Current.Contains(component);

    public bool Contains<T>(T component, bool inherit) where T : IComponent => Current.Contains(component, inherit);

    public bool Any<T>() where T : IComponent => Current.Any<T>();

    public bool Any<T>(bool inherit) where T : IComponent => Current.Any<T>(inherit);
}