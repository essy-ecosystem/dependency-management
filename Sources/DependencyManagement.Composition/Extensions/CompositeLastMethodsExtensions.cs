using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;
using DependencyManagement.Composition.Enums;
using DependencyManagement.Composition.Exceptions;

namespace DependencyManagement.Composition.Extensions;

public static class CompositeLastMethodsExtensions
{
    public static T? TryLast<T>(this IReadOnlyComposite composite) where T : class, IComponent
    {
        var components = composite.All<T>();

        return components.Count != 0
            ? components[^1]
            : null;
    }

    public static T? TryLast<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current) return composite.TryLast<T>();

        var current = composite;

        do
        {
            if (current.TryLast<T>() is not null and var last) return last;
            current = current.Father;
        } while (current is not null);

        return null;
    }

    public static T? TryLast<T>(this IReadOnlyComposite composite, Predicate<T> predicate) where T : class, IComponent
    {
        return composite.All<T>().LastOrDefault(component => predicate(component));
    }

    public static T? TryLast<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current) return composite.TryLast(predicate);

        var current = composite;

        do
        {
            if (current.TryLast(predicate) is not null and var last) return last;
            current = current.Father;
        } while (current is not null);

        return null;
    }

    public static T Last<T>(this IReadOnlyComposite composite) where T : class, IComponent
    {
        return composite.TryLast<T>() ?? throw new EmptySequenceDependencyManagementException();
    }

    public static T Last<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return composite.TryLast<T>(strategy) ?? throw new EmptySequenceDependencyManagementException();
    }

    public static T Last<T>(this IReadOnlyComposite composite, Predicate<T> predicate) where T : class, IComponent
    {
        return composite.TryLast(predicate) ?? throw new EmptySequenceDependencyManagementException();
    }

    public static T Last<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        return composite.TryLast(strategy, predicate) ?? throw new EmptySequenceDependencyManagementException();
    }

    public static ILazyComponent<T>? TryLastLazy<T>(this IReadOnlyComposite composite) where T : class, IComponent
    {
        return composite.TryLast<ILazyComponent<T>>();
    }

    public static ILazyComponent<T>? TryLastLazy<T>(this IReadOnlyComposite composite,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        return composite.TryLast<ILazyComponent<T>>(strategy);
    }

    public static ILazyComponent<T>? TryLastLazy<T>(this IReadOnlyComposite composite,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return composite.TryLast(predicate);
    }

    public static ILazyComponent<T>? TryLastLazy<T>(this IReadOnlyComposite composite,
        CompositeTraversalStrategy strategy, Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return composite.TryLast(strategy, predicate);
    }

    public static ILazyComponent<T> LastLazy<T>(this IReadOnlyComposite composite) where T : class, IComponent
    {
        return composite.Last<ILazyComponent<T>>();
    }

    public static ILazyComponent<T> LastLazy<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return composite.Last<ILazyComponent<T>>(strategy);
    }

    public static ILazyComponent<T> LastLazy<T>(this IReadOnlyComposite composite,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return composite.Last(predicate);
    }

    public static ILazyComponent<T> LastLazy<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return composite.Last(strategy, predicate);
    }
}