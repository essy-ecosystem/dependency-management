namespace DependencyManagement.Injection.Extensions;

using Composition.Containers;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class CompositeCountMethodsExtensions
{
    public static int CountTarget<T>(this IReadOnlyContainer container) where T : class
    {
        return container.Count<ITarget<T>>();
    }

    public static int CountTarget<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class
    {
        return container.Count<ITarget<T>>(strategy);
    }

    public static int CountTarget<T>(this IReadOnlyContainer container, Predicate<ITarget<T>> predicate) where T : class
    {
        return container.Count(predicate);
    }

    public static int CountTarget<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return container.Count(strategy, predicate);
    }

    public static int CountInstance<T>(this IReadOnlyContainer container) where T : class
    {
        return container.CountTarget<T>();
    }

    public static int CountInstance<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class
    {
        return container.CountTarget<T>(strategy);
    }

    public static int CountInstance<T>(this IReadOnlyContainer container, Predicate<T> predicate) where T : class
    {
        return container.WhereInstance(predicate).Count;
    }

    public static int CountInstance<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return container.WhereInstance(strategy, predicate).Count;
    }
}