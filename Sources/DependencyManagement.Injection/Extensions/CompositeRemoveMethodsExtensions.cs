namespace DependencyManagement.Injection.Extensions;

using Composition.Components;
using Composition.Composites;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class CompositeRemoveMethodsExtensions
{
    public static bool RemoveTarget<T>(this IComposite composite, ITarget<T> component) where T : class
    {
        return composite.Remove(component);
    }

    public static bool RemoveTarget<T>(this IComposite composite, ITarget<T> component,
        CompositeTraversalStrategy strategy) where T : class
    {
        return composite.Remove(component, strategy);
    }

    public static bool RemoveTarget<T>(this IComposite composite, Predicate<T> predicate) where T : class, IComponent
    {
        return composite.Remove(predicate);
    }

    public static bool RemoveTarget<T>(this IComposite composite, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        return composite.Remove(strategy, predicate);
    }
}