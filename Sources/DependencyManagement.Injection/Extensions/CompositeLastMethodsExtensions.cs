using DependencyManagement.Composition.Composites;
using DependencyManagement.Composition.Enums;
using DependencyManagement.Composition.Exceptions;
using DependencyManagement.Composition.Extensions;
using DependencyManagement.Injection.Targets;

namespace DependencyManagement.Injection.Extensions;

public static class CompositeLastMethodsExtensions
{
    public static ITarget<T>? TryLastTarget<T>(this IReadOnlyComposite composite) where T : class
    {
        return composite.TryLast<ITarget<T>>();
    }

    public static ITarget<T>? TryLastTarget<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class
    {
        return composite.TryLast<ITarget<T>>(strategy);
    }

    public static ITarget<T>? TryLastTarget<T>(this IReadOnlyComposite composite, Predicate<ITarget<T>> predicate)
        where T : class
    {
        return composite.TryLast(predicate);
    }

    public static ITarget<T>? TryLastTarget<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return composite.TryLast(strategy, predicate);
    }

    public static ITarget<T> LastTarget<T>(this IReadOnlyComposite composite) where T : class
    {
        return composite.Last<ITarget<T>>();
    }

    public static ITarget<T> LastTarget<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class
    {
        return composite.Last<ITarget<T>>(strategy);
    }

    public static ITarget<T> LastTarget<T>(this IReadOnlyComposite composite, Predicate<ITarget<T>> predicate)
        where T : class
    {
        return composite.Last(predicate);
    }

    public static ITarget<T> LastTarget<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return composite.Last(strategy, predicate);
    }

    public static T? TryLastInstance<T>(this IReadOnlyComposite composite) where T : class
    {
        return composite.TryLastTarget<T>()?.GetInstance(composite);
    }

    public static T? TryLastInstance<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class
    {
        return composite.TryLastTarget<T>(strategy)?.GetInstance(composite);
    }

    public static T? TryLastInstance<T>(this IReadOnlyComposite composite, Predicate<T> predicate) where T : class
    {
        return composite.AllInstance<T>().LastOrDefault(instance => predicate(instance));
    }

    public static T? TryLastInstance<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class
    {
        if (strategy == CompositeTraversalStrategy.Current) return composite.TryLastInstance(predicate);

        var current = composite;

        do
        {
            if (current.TryLastInstance(predicate) is not null and var last) return last;
            current = current.Father;
        } while (current is not null);

        return null;
    }

    public static T LastInstance<T>(this IReadOnlyComposite composite) where T : class
    {
        return composite.LastTarget<T>().GetInstance(composite);
    }

    public static T LastInstance<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class
    {
        return composite.LastTarget<T>(strategy).GetInstance(composite);
    }

    public static T LastInstance<T>(this IReadOnlyComposite composite, Predicate<T> predicate) where T : class
    {
        return composite.TryLastInstance(predicate) ?? throw new EmptySequenceDependencyManagementException();
    }

    public static T LastInstance<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class
    {
        return composite.TryLastInstance(strategy, predicate) ?? throw new EmptySequenceDependencyManagementException();
    }
}