namespace DependencyManagement.Composition.Extensions;

using Components;
using Containers;
using Enums;
using Exceptions;
using Utils;

public static class CompositeFirstMethodsExtensions
{
    public static T? TryFirst<T>(this IReadOnlyContainer container) where T : class, IComponent
    {
        var components = container.All<T>();

        return components.Count != 0
            ? components[0]
            : null;
    }

    public static T? TryFirst<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current || container.Father is null) return container.TryFirst<T>();
        if (strategy == CompositeTraversalStrategy.Initial) return ContainerTreeUtils.GetLast(container).TryFirst<T>();

        var composites = ContainerTreeUtils.GetTree(container);

        for (var i = composites.Count - 1; i >= 0; i--)
        {
            var current = composites[i];
            if (current.TryFirst<T>() is not null and var first) return first;
        }

        return null;
    }

    public static T? TryFirst<T>(this IReadOnlyContainer container, Predicate<T> predicate) where T : class, IComponent
    {
        return container.All<T>().FirstOrDefault(component => predicate(component));
    }

    public static T? TryFirst<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current || container.Father is null)
        {
            return container.TryFirst(predicate);
        }

        if (strategy == CompositeTraversalStrategy.Initial)
        {
            return ContainerTreeUtils.GetLast(container).TryFirst(predicate);
        }

        var composites = ContainerTreeUtils.GetTree(container);

        for (var i = composites.Count - 1; i >= 0; i--)
        {
            var current = composites[i];
            if (current.TryFirst(predicate) is not null and var first) return first;
        }

        return null;
    }

    public static T First<T>(this IReadOnlyContainer container) where T : class, IComponent
    {
        return container.TryFirst<T>() ?? throw new EmptySequenceDependencyManagementException();
    }

    public static T First<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return container.TryFirst<T>(strategy) ?? throw new EmptySequenceDependencyManagementException();
    }

    public static T First<T>(this IReadOnlyContainer container, Predicate<T> predicate) where T : class, IComponent
    {
        return container.TryFirst(predicate) ?? throw new EmptySequenceDependencyManagementException();
    }

    public static T First<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        return container.TryFirst(strategy, predicate) ?? throw new EmptySequenceDependencyManagementException();
    }

    public static ILazyComponent<T>? TryFirstLazy<T>(this IReadOnlyContainer container) where T : class, IComponent
    {
        return container.TryFirst<ILazyComponent<T>>();
    }

    public static ILazyComponent<T>? TryFirstLazy<T>(this IReadOnlyContainer container,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        return container.TryFirst<ILazyComponent<T>>(strategy);
    }

    public static ILazyComponent<T>? TryFirstLazy<T>(this IReadOnlyContainer container,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return container.TryFirst(predicate);
    }

    public static ILazyComponent<T>? TryFirstLazy<T>(this IReadOnlyContainer container,
        CompositeTraversalStrategy strategy, Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return container.TryFirst(strategy, predicate);
    }

    public static ILazyComponent<T> FirstLazy<T>(this IReadOnlyContainer container) where T : class, IComponent
    {
        return container.First<ILazyComponent<T>>();
    }

    public static ILazyComponent<T> FirstLazy<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return container.First<ILazyComponent<T>>(strategy);
    }

    public static ILazyComponent<T> FirstLazy<T>(this IReadOnlyContainer container,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return container.First(predicate);
    }

    public static ILazyComponent<T> FirstLazy<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return container.First(strategy, predicate);
    }
}