namespace DependencyManagement.Injection.Extensions;

using Composition.Composites;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class CompositeCountMethodsExtensions
{
    public static int CountTarget<T>(this IReadOnlyComposite composite) where T : class
    {
        return composite.Count<ITarget<T>>();
    }

    public static int CountTarget<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class
    {
        return composite.Count<ITarget<T>>(strategy);
    }

    public static int CountTarget<T>(this IReadOnlyComposite composite, Predicate<ITarget<T>> predicate) where T : class
    {
        return composite.Count(predicate);
    }

    public static int CountTarget<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return composite.Count(strategy, predicate);
    }

    public static int CountInstance<T>(this IReadOnlyComposite composite) where T : class
    {
        return composite.CountTarget<T>();
    }

    public static int CountInstance<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class
    {
        return composite.CountTarget<T>(strategy);
    }

    public static int CountInstance<T>(this IReadOnlyComposite composite, Predicate<T> predicate) where T : class
    {
        return composite.WhereInstance(predicate).Count;
    }

    public static int CountInstance<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return composite.WhereInstance(strategy, predicate).Count;
    }
}