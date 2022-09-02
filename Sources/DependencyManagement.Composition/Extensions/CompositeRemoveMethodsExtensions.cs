namespace DependencyManagement.Composition.Extensions;

using Components;
using Composites;
using Enums;

public static class CompositeRemoveMethodsExtensions
{
    public static bool Remove<T>(this IComposite composite, T component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (composite.Remove(component)) return true;
        if (strategy == CompositeTraversalStrategy.Current) return false;

        while (composite.Father is not null)
        {
            composite = composite.Father!;
            if (composite.Remove(component)) return true;
        }

        return false;
    }

    public static bool Remove<T>(this IComposite composite, Predicate<T> predicate) where T : class, IComponent
    {
        var components = composite.Where(predicate);
        var result = false;

        foreach (var component in components)
        {
            if (composite.Remove(component))
            {
                result = true;
            }
        }

        return result;
    }

    public static bool Remove<T>(this IComposite composite, CompositeTraversalStrategy strategy, Predicate<T> predicate)
        where T : class, IComponent
    {
        var result = composite.Remove(predicate);

        if (strategy == CompositeTraversalStrategy.Current) return result;

        while (composite.Father is not null)
        {
            composite = composite.Father!;
            if (composite.Remove(predicate)) result = true;
        }

        return result;
    }

    public static bool RemoveLazy<T>(this IComposite composite, ILazyComponent<T> component) where T : class, IComponent
    {
        return composite.Remove(component);
    }

    public static bool RemoveLazy<T>(this IComposite composite, ILazyComponent<T> component,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        return composite.Remove(component, strategy);
    }

    public static bool RemoveLazy<T>(this IComposite composite, T component) where T : class, IComponent
    {
        return composite.RemoveLazy(composite
            .FirstLazy<T>(lazy => lazy.IsValueCreated && lazy.Value == component));
    }

    public static bool RemoveLazy<T>(this IComposite composite, T component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return composite.RemoveLazy(composite
            .FirstLazy<T>(strategy, lazy => lazy.IsValueCreated && lazy.Value == component), strategy);
    }

    public static bool RemoveLazy<T>(this IComposite composite, Predicate<ILazyComponent<T>> predicate)
        where T : class, IComponent
    {
        return composite.Remove(predicate);
    }

    public static bool RemoveLazy<T>(this IComposite composite, CompositeTraversalStrategy strategy,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return composite.Remove(strategy, predicate);
    }
}