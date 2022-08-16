using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;
using DependencyManagement.Composition.Enums;

namespace DependencyManagement.Composition.Extensions;

public static class CompositeContainsMethodsExtensions
{
    public static bool Contains<T>(this IReadOnlyComposite composite, T component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (composite.Contains(component)) return true;
        if (strategy == CompositeTraversalStrategy.Current) return false;

        while (composite.Father is not null)
        {
            composite = composite.Father!;
            if (composite.Contains(component)) return true;
        }

        return false;
    }

    public static bool Contains<T>(this IReadOnlyComposite composite, Predicate<T> predicate)
        where T : class, IComponent
    {
        return composite.Where(predicate).Any();
    }

    public static bool Contains<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        return composite.Where(strategy, predicate).Any();
    }

    public static bool ContainsLazy<T>(this IReadOnlyComposite composite, ILazyComponent<T> component)
        where T : class, IComponent
    {
        return composite.Contains(component);
    }

    public static bool ContainsLazy<T>(this IReadOnlyComposite composite, ILazyComponent<T> component,
        CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return composite.Contains(component, strategy);
    }

    public static bool ContainsLazy<T>(this IReadOnlyComposite composite, T component)
        where T : class, IComponent
    {
        return composite.ContainsLazy<T>(lazy => lazy.IsValueCreated && lazy.Value == component);
    }

    public static bool ContainsLazy<T>(this IReadOnlyComposite composite, T component,
        CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return composite.ContainsLazy<T>(strategy, lazy => lazy.IsValueCreated && lazy.Value == component);
    }

    public static bool ContainsLazy<T>(this IReadOnlyComposite composite, Predicate<ILazyComponent<T>> predicate)
        where T : class, IComponent
    {
        return composite.Contains(predicate);
    }

    public static bool ContainsLazy<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return composite.Contains(strategy, predicate);
    }
}