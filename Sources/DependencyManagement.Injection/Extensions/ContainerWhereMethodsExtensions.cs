namespace DependencyManagement.Injection.Extensions;

using Composition.Containers;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class ContainerWhereMethodsExtensions
{
    public static IReadOnlyList<ITarget<T>> WhereTarget<T>(this IReadOnlyContainer container,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return container.Where(predicate).ToArray();
    }

    public static IReadOnlyList<ITarget<T>> WhereTarget<T>(this IReadOnlyContainer container,
        TraversalStrategy strategy, Predicate<ITarget<T>> predicate) where T : class
    {
        return container.Where(strategy, predicate).ToArray();
    }

    public static IReadOnlyList<T> WhereInstance<T>(this IReadOnlyContainer container, Predicate<T> predicate)
        where T : class
    {
        return container.AllInstance<T>().Where(instance => predicate(instance)).ToArray();
    }

    public static IReadOnlyList<T> WhereInstance<T>(this IReadOnlyContainer container,
        TraversalStrategy strategy, Predicate<T> predicate) where T : class
    {
        return container.AllInstance<T>(strategy).Where(instance => predicate(instance)).ToArray();
    }
}