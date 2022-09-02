namespace DependencyManagement.Injection.Extensions;

using Composition.Composites;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class CompositeWhereMethodsExtensions
{
    public static IReadOnlyList<ITarget<T>> WhereTarget<T>(this IReadOnlyComposite composite,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return composite.Where(predicate).ToArray();
    }

    public static IReadOnlyList<ITarget<T>> WhereTarget<T>(this IReadOnlyComposite composite,
        CompositeTraversalStrategy strategy, Predicate<ITarget<T>> predicate) where T : class
    {
        return composite.Where(strategy, predicate).ToArray();
    }

    public static IReadOnlyList<T> WhereInstance<T>(this IReadOnlyComposite composite, Predicate<T> predicate)
        where T : class
    {
        return composite.AllInstance<T>().Where(instance => predicate(instance)).ToArray();
    }

    public static IReadOnlyList<T> WhereInstance<T>(this IReadOnlyComposite composite,
        CompositeTraversalStrategy strategy, Predicate<T> predicate) where T : class
    {
        return composite.AllInstance<T>(strategy).Where(instance => predicate(instance)).ToArray();
    }
}