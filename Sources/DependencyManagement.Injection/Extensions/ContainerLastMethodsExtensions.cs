namespace DependencyManagement.Injection.Extensions;

using Composition.Containers;
using Composition.Enums;
using Composition.Extensions;
using Composition.Utils;
using Targets;

public static class ContainerLastMethodsExtensions
{
    public static ITarget<T>? TryLastTarget<T>(this IReadOnlyContainer container) where T : class
    {
        return container.TryLast<ITarget<T>>();
    }

    public static ITarget<T>? TryLastTarget<T>(this IReadOnlyContainer container, TraversalStrategy strategy)
        where T : class
    {
        return container.TryLast<ITarget<T>>(strategy);
    }

    public static ITarget<T>? TryLastTarget<T>(this IReadOnlyContainer container, Predicate<ITarget<T>> predicate)
        where T : class
    {
        return container.TryLast(predicate);
    }

    public static ITarget<T>? TryLastTarget<T>(this IReadOnlyContainer container, TraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return container.TryLast(strategy, predicate);
    }

    public static ITarget<T> LastTarget<T>(this IReadOnlyContainer container) where T : class
    {
        return container.Last<ITarget<T>>();
    }

    public static ITarget<T> LastTarget<T>(this IReadOnlyContainer container, TraversalStrategy strategy)
        where T : class
    {
        return container.Last<ITarget<T>>(strategy);
    }

    public static ITarget<T> LastTarget<T>(this IReadOnlyContainer container, Predicate<ITarget<T>> predicate)
        where T : class
    {
        return container.Last(predicate);
    }

    public static ITarget<T> LastTarget<T>(this IReadOnlyContainer container, TraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return container.Last(strategy, predicate);
    }

    public static T? TryLastInstance<T>(this IReadOnlyContainer container) where T : class
    {
        return container.TryLastTarget<T>()?.ProvideInstance(container);
    }

    public static T? TryLastInstance<T>(this IReadOnlyContainer container, TraversalStrategy strategy)
        where T : class
    {
        return container.TryLastTarget<T>(strategy)?.ProvideInstance(container);
    }

    public static T? TryLastInstance<T>(this IReadOnlyContainer container, Predicate<T> predicate) where T : class
    {
        return container.AllInstance<T>().LastOrDefault(instance => predicate(instance));
    }

    public static T? TryLastInstance<T>(this IReadOnlyContainer container, TraversalStrategy strategy,
        Predicate<T> predicate) where T : class
    {
        if (strategy == TraversalStrategy.Current) return container.TryLastInstance(predicate);

        if (strategy == TraversalStrategy.Initial)
        {
            return TraversalService.GetInitial(container).TryLastInstance(predicate);
        }

        var current = container;

        do
        {
            if (current.TryLastInstance(predicate) is not null and var last) return last;
            current = current.Father;
        } while (current is not null);

        return null;
    }

    public static T LastInstance<T>(this IReadOnlyContainer container) where T : class
    {
        return container.LastTarget<T>().ProvideInstance(container);
    }

    public static T LastInstance<T>(this IReadOnlyContainer container, TraversalStrategy strategy)
        where T : class
    {
        return container.LastTarget<T>(strategy).ProvideInstance(container);
    }

    public static T LastInstance<T>(this IReadOnlyContainer container, Predicate<T> predicate) where T : class
    {
        return container.TryLastInstance(predicate)
            ?? throw new InvalidOperationException("The source sequence is empty.");
    }

    public static T LastInstance<T>(this IReadOnlyContainer container, TraversalStrategy strategy,
        Predicate<T> predicate) where T : class
    {
        return container.TryLastInstance(strategy, predicate)
            ?? throw new InvalidOperationException("The source sequence is empty.");
    }
}