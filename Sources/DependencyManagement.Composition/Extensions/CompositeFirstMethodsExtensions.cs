namespace DependencyManagement.Composition.Extensions;

using Components;
using Composites;
using Enums;
using Exceptions;
using Utils;

public static class CompositeFirstMethodsExtensions
{
    public static T? TryFirst<T>(this IReadOnlyComposite composite) where T : class, IComponent
    {
        var components = composite.All<T>();

        return components.Count != 0
            ? components[0]
            : null;
    }

    public static T? TryFirst<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current || composite.Father is null) return composite.TryFirst<T>();

        var composites = CompositeTreeUtils.GetTree(composite);

        for (var i = composites.Count - 1; i >= 0; i--)
        {
            var current = composites[i];
            if (current.TryFirst<T>() is not null and var first) return first;
        }

        return null;
    }

    public static T? TryFirst<T>(this IReadOnlyComposite composite, Predicate<T> predicate) where T : class, IComponent
    {
        return composite.All<T>().FirstOrDefault(component => predicate(component));
    }

    public static T? TryFirst<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current || composite.Father is null)
        {
            return composite.TryFirst(predicate);
        }

        var composites = CompositeTreeUtils.GetTree(composite);

        for (var i = composites.Count - 1; i >= 0; i--)
        {
            var current = composites[i];
            if (current.TryFirst(predicate) is not null and var first) return first;
        }

        return null;
    }

    public static T First<T>(this IReadOnlyComposite composite) where T : class, IComponent
    {
        return composite.TryFirst<T>() ?? throw new EmptySequenceDependencyManagementException();
    }

    public static T First<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return composite.TryFirst<T>(strategy) ?? throw new EmptySequenceDependencyManagementException();
    }

    public static T First<T>(this IReadOnlyComposite composite, Predicate<T> predicate) where T : class, IComponent
    {
        return composite.TryFirst(predicate) ?? throw new EmptySequenceDependencyManagementException();
    }

    public static T First<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        return composite.TryFirst(strategy, predicate) ?? throw new EmptySequenceDependencyManagementException();
    }

    public static ILazyComponent<T>? TryFirstLazy<T>(this IReadOnlyComposite composite) where T : class, IComponent
    {
        return composite.TryFirst<ILazyComponent<T>>();
    }

    public static ILazyComponent<T>? TryFirstLazy<T>(this IReadOnlyComposite composite,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        return composite.TryFirst<ILazyComponent<T>>(strategy);
    }

    public static ILazyComponent<T>? TryFirstLazy<T>(this IReadOnlyComposite composite,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return composite.TryFirst(predicate);
    }

    public static ILazyComponent<T>? TryFirstLazy<T>(this IReadOnlyComposite composite,
        CompositeTraversalStrategy strategy, Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return composite.TryFirst(strategy, predicate);
    }

    public static ILazyComponent<T> FirstLazy<T>(this IReadOnlyComposite composite) where T : class, IComponent
    {
        return composite.First<ILazyComponent<T>>();
    }

    public static ILazyComponent<T> FirstLazy<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return composite.First<ILazyComponent<T>>(strategy);
    }

    public static ILazyComponent<T> FirstLazy<T>(this IReadOnlyComposite composite,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return composite.First(predicate);
    }

    public static ILazyComponent<T> FirstLazy<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return composite.First(strategy, predicate);
    }
}