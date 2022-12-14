namespace DependencyManagement.Composition.Extensions;

using Components;
using Containers;
using Enums;
using Utils;

public static class ContainerLastMethodsExtensions
{
    public static T? TryLast<T>(this IReadOnlyContainer container) where T : class, IComponent
    {
        var components = container.All<T>();

        return components.Count != 0
            ? components[^1]
            : null;
    }

    public static T? TryLast<T>(this IReadOnlyContainer container, TraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == TraversalStrategy.Current) return container.TryLast<T>();
        if (strategy == TraversalStrategy.Initial) return TraversalService.GetInitial(container).TryLast<T>();

        var current = container;

        do
        {
            if (current.TryLast<T>() is not null and var last) return last;
            current = current.Father;
        } while (current is not null);

        return null;
    }

    public static T? TryLast<T>(this IReadOnlyContainer container, Predicate<T> predicate) where T : class, IComponent
    {
        return container.All<T>().LastOrDefault(component => predicate(component));
    }

    public static T? TryLast<T>(this IReadOnlyContainer container, TraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        if (strategy == TraversalStrategy.Current) return container.TryLast(predicate);
        if (strategy == TraversalStrategy.Initial)
        {
            return TraversalService.GetInitial(container).TryLast(predicate);
        }

        var current = container;

        do
        {
            if (current.TryLast(predicate) is not null and var last) return last;
            current = current.Father;
        } while (current is not null);

        return null;
    }

    public static T Last<T>(this IReadOnlyContainer container) where T : class, IComponent
    {
        return container.TryLast<T>()
            ?? throw new InvalidOperationException("The source sequence is empty.");
    }

    public static T Last<T>(this IReadOnlyContainer container, TraversalStrategy strategy)
        where T : class, IComponent
    {
        return container.TryLast<T>(strategy)
            ?? throw new InvalidOperationException("The source sequence is empty.");
    }

    public static T Last<T>(this IReadOnlyContainer container, Predicate<T> predicate) where T : class, IComponent
    {
        return container.TryLast(predicate)
            ?? throw new InvalidOperationException("The source sequence is empty.");
    }

    public static T Last<T>(this IReadOnlyContainer container, TraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        return container.TryLast(strategy, predicate)
            ?? throw new InvalidOperationException("The source sequence is empty.");
    }

    public static ILazyComponent<T>? TryLastLazy<T>(this IReadOnlyContainer container) where T : class, IComponent
    {
        return container.TryLast<ILazyComponent<T>>();
    }

    public static ILazyComponent<T>? TryLastLazy<T>(this IReadOnlyContainer container,
        TraversalStrategy strategy) where T : class, IComponent
    {
        return container.TryLast<ILazyComponent<T>>(strategy);
    }

    public static ILazyComponent<T>? TryLastLazy<T>(this IReadOnlyContainer container,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return container.TryLast(predicate);
    }

    public static ILazyComponent<T>? TryLastLazy<T>(this IReadOnlyContainer container,
        TraversalStrategy strategy, Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return container.TryLast(strategy, predicate);
    }

    public static ILazyComponent<T> LastLazy<T>(this IReadOnlyContainer container) where T : class, IComponent
    {
        return container.Last<ILazyComponent<T>>();
    }

    public static ILazyComponent<T> LastLazy<T>(this IReadOnlyContainer container, TraversalStrategy strategy)
        where T : class, IComponent
    {
        return container.Last<ILazyComponent<T>>(strategy);
    }

    public static ILazyComponent<T> LastLazy<T>(this IReadOnlyContainer container,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return container.Last(predicate);
    }

    public static ILazyComponent<T> LastLazy<T>(this IReadOnlyContainer container, TraversalStrategy strategy,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return container.Last(strategy, predicate);
    }
}