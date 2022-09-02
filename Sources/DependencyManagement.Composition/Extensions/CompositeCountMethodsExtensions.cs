namespace DependencyManagement.Composition.Extensions;

using Components;
using Containers;
using Enums;

public static class CompositeCountMethodsExtensions
{
    public static int Count<T>(this IReadOnlyContainer container) where T : class, IComponent
    {
        return container.All<T>().Count;
    }

    public static int Count<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return container.All<T>(strategy).Count;
    }

    public static int Count<T>(this IReadOnlyContainer container, Predicate<T> predicate) where T : class, IComponent
    {
        return container.Where(predicate).Count;
    }

    public static int Count<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        return container.Where(strategy, predicate).Count;
    }

    public static int CountLazy<T>(this IReadOnlyContainer container) where T : class, IComponent
    {
        return container.Count<ILazyComponent<T>>();
    }

    public static int CountLazy<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return container.Count<ILazyComponent<T>>(strategy);
    }

    public static int CountLazy<T>(this IReadOnlyContainer container, Predicate<ILazyComponent<T>> predicate)
        where T : class, IComponent
    {
        return container.Count(predicate);
    }

    public static int CountLazy<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return container.Count(strategy, predicate);
    }
}