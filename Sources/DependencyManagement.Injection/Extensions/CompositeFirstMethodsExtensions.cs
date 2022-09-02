namespace DependencyManagement.Injection.Extensions;

using Composition.Containers;
using Composition.Enums;
using Composition.Exceptions;
using Composition.Extensions;
using Composition.Utils;
using Targets;

public static class CompositeFirstMethodsExtensions
{
    public static ITarget<T>? TryFirstTarget<T>(this IReadOnlyContainer container) where T : class
    {
        return container.TryFirst<ITarget<T>>();
    }

    public static ITarget<T>? TryFirstTarget<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class
    {
        return container.TryFirst<ITarget<T>>(strategy);
    }

    public static ITarget<T>? TryFirstTarget<T>(this IReadOnlyContainer container, Predicate<ITarget<T>> predicate)
        where T : class
    {
        return container.TryFirst(predicate);
    }

    public static ITarget<T>? TryFirstTarget<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return container.TryFirst(strategy, predicate);
    }

    public static ITarget<T> FirstTarget<T>(this IReadOnlyContainer container) where T : class
    {
        return container.First<ITarget<T>>();
    }

    public static ITarget<T> FirstTarget<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class
    {
        return container.First<ITarget<T>>(strategy);
    }

    public static ITarget<T> FirstTarget<T>(this IReadOnlyContainer container, Predicate<ITarget<T>> predicate)
        where T : class
    {
        return container.First(predicate);
    }

    public static ITarget<T> FirstTarget<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return container.First(strategy, predicate);
    }

    public static T? TryFirstInstance<T>(this IReadOnlyContainer container) where T : class
    {
        return container.TryFirstTarget<T>()?.GetInstance(container);
    }

    public static T? TryFirstInstance<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class
    {
        return container.TryFirstTarget<T>(strategy)?.GetInstance(container);
    }

    public static T? TryFirstInstance<T>(this IReadOnlyContainer container, Predicate<T> predicate) where T : class
    {
        return container.AllInstance<T>().FirstOrDefault(instance => predicate(instance));
    }

    public static T? TryFirstInstance<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class
    {
        if (strategy == CompositeTraversalStrategy.Current || container.Father is null)
        {
            return container.TryFirstInstance(predicate);
        }

        if (strategy == CompositeTraversalStrategy.Initial)
        {
            return ContainerTreeUtils.GetLast(container).TryFirstInstance(predicate);
        }

        var composites = ContainerTreeUtils.GetTree(container);

        for (var i = composites.Count - 1; i >= 0; i--)
        {
            var current = composites[i];
            if (current.TryFirstInstance(predicate) is not null and var first) return first;
        }

        return null;
    }

    public static T FirstInstance<T>(this IReadOnlyContainer container) where T : class
    {
        return container.FirstTarget<T>().GetInstance(container);
    }

    public static T FirstInstance<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class
    {
        return container.FirstTarget<T>(strategy).GetInstance(container);
    }

    public static T FirstInstance<T>(this IReadOnlyContainer container, Predicate<T> predicate) where T : class
    {
        return container.TryFirstInstance(predicate) ?? throw new EmptySequenceDependencyManagementException();
    }

    public static T FirstInstance<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class
    {
        return container.TryFirstInstance(strategy, predicate) ??
            throw new EmptySequenceDependencyManagementException();
    }
}