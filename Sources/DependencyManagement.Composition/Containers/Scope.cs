namespace DependencyManagement.Composition.Containers;

using Components;
using Core.Disposables;
using Core.Utils;

public class Scope : AsyncDisposableObject, IScope
{
    public Scope(IReadOnlyContainer container)
    {
        Thrower.ThrowIfArgumentNull(container);

        Current = container is IScope scope 
            ? scope.Current 
            : container;
    }
    
    public IReadOnlyContainer Current { get; }

    public IReadOnlyContainer? Father => Current.Father;
        
    IReadOnlyContainer? IReadOnlyContainer.Father => Father;

    public IReadOnlyList<T> All<T>() where T : class, IComponent
    {
        return Current.All<T>();
    }

    public bool Contains<T>(T component) where T : class, IComponent
    {
        return Current.Contains(component);
    }

    public bool Any<T>() where T : class, IComponent
    {
        return Current.Any<T>();
    }
}