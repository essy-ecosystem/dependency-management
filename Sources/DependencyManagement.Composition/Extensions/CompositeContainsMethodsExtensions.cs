namespace DependencyManagement.Composition.Extensions;

using Components;
using Containers;
using Enums;
using Utils;

public static class CompositeContainsMethodsExtensions
{
    public static bool Contains<T>(this IReadOnlyContainer container, T component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current) return container.Contains(component);
        if (strategy == CompositeTraversalStrategy.Initial) return ContainerTreeUtils.GetLast(container).Contains(component);
        
        if (container.Contains(component)) return true;

        while (container.Father is not null)
        {
            container = container.Father!;
            if (container.Contains(component)) return true;
        }

        return false;
    }

    public static bool Contains<T>(this IReadOnlyContainer container, Predicate<T> predicate)
        where T : class, IComponent
    {
        return container.Where(predicate).Any();
    }

    public static bool Contains<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        return container.Where(strategy, predicate).Any();
    }

    public static bool ContainsLazy<T>(this IReadOnlyContainer container, ILazyComponent<T> component)
        where T : class, IComponent
    {
        return container.Contains(component);
    }

    public static bool ContainsLazy<T>(this IReadOnlyContainer container, ILazyComponent<T> component,
        CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return container.Contains(component, strategy);
    }

    public static bool ContainsLazy<T>(this IReadOnlyContainer container, T component)
        where T : class, IComponent
    {
        return container.ContainsLazy<T>(lazy => lazy.IsValueCreated && lazy.Value == component);
    }

    public static bool ContainsLazy<T>(this IReadOnlyContainer container, T component,
        CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return container.ContainsLazy<T>(strategy, lazy => lazy.IsValueCreated && lazy.Value == component);
    }

    public static bool ContainsLazy<T>(this IReadOnlyContainer container, Predicate<ILazyComponent<T>> predicate)
        where T : class, IComponent
    {
        return container.Contains(predicate);
    }

    public static bool ContainsLazy<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return container.Contains(strategy, predicate);
    }
}