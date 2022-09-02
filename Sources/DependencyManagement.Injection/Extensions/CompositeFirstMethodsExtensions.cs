namespace DependencyManagement.Injection.Extensions;

using Composition.Composites;
using Composition.Enums;
using Composition.Exceptions;
using Composition.Extensions;
using Composition.Utils;
using Targets;

public static class CompositeFirstMethodsExtensions
{
    public static ITarget<T>? TryFirstTarget<T>(this IReadOnlyComposite composite) where T : class
    {
        return composite.TryFirst<ITarget<T>>();
    }

    public static ITarget<T>? TryFirstTarget<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class
    {
        return composite.TryFirst<ITarget<T>>(strategy);
    }

    public static ITarget<T>? TryFirstTarget<T>(this IReadOnlyComposite composite, Predicate<ITarget<T>> predicate)
        where T : class
    {
        return composite.TryFirst(predicate);
    }

    public static ITarget<T>? TryFirstTarget<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return composite.TryFirst(strategy, predicate);
    }

    public static ITarget<T> FirstTarget<T>(this IReadOnlyComposite composite) where T : class
    {
        return composite.First<ITarget<T>>();
    }

    public static ITarget<T> FirstTarget<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class
    {
        return composite.First<ITarget<T>>(strategy);
    }

    public static ITarget<T> FirstTarget<T>(this IReadOnlyComposite composite, Predicate<ITarget<T>> predicate)
        where T : class
    {
        return composite.First(predicate);
    }

    public static ITarget<T> FirstTarget<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return composite.First(strategy, predicate);
    }

    public static T? TryFirstInstance<T>(this IReadOnlyComposite composite) where T : class
    {
        return composite.TryFirstTarget<T>()?.GetInstance(composite);
    }

    public static T? TryFirstInstance<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class
    {
        return composite.TryFirstTarget<T>(strategy)?.GetInstance(composite);
    }

    public static T? TryFirstInstance<T>(this IReadOnlyComposite composite, Predicate<T> predicate) where T : class
    {
        return composite.AllInstance<T>().FirstOrDefault(instance => predicate(instance));
    }

    public static T? TryFirstInstance<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class
    {
        if (strategy == CompositeTraversalStrategy.Current || composite.Father is null)
        {
            return composite.TryFirstInstance(predicate);
        }

        var composites = CompositeTreeUtils.GetTree(composite);

        for (var i = composites.Count - 1; i >= 0; i--)
        {
            var current = composites[i];
            if (current.TryFirstInstance(predicate) is not null and var first) return first;
        }

        return null;
    }

    public static T FirstInstance<T>(this IReadOnlyComposite composite) where T : class
    {
        return composite.FirstTarget<T>().GetInstance(composite);
    }

    public static T FirstInstance<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class
    {
        return composite.FirstTarget<T>(strategy).GetInstance(composite);
    }

    public static T FirstInstance<T>(this IReadOnlyComposite composite, Predicate<T> predicate) where T : class
    {
        return composite.TryFirstInstance(predicate) ?? throw new EmptySequenceDependencyManagementException();
    }

    public static T FirstInstance<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class
    {
        return composite.TryFirstInstance(strategy, predicate) ??
            throw new EmptySequenceDependencyManagementException();
    }
}