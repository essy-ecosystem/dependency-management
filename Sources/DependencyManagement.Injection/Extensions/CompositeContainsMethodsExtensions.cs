namespace DependencyManagement.Injection.Extensions;

using Composition.Containers;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class CompositeContainsMethodsExtensions
{
    public static bool ContainsTarget<T>(this IReadOnlyContainer container, ITarget<T> component) where T : class
    {
        return container.Contains(component);
    }

    public static bool ContainsTarget<T>(this IReadOnlyContainer container, ITarget<T> component,
        CompositeTraversalStrategy strategy) where T : class
    {
        return container.Contains(component, strategy);
    }

    public static bool ContainsTarget<T>(this IReadOnlyContainer container, Predicate<ITarget<T>> predicate)
        where T : class
    {
        return container.Contains(predicate);
    }

    public static bool ContainsTarget<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return container.Contains(strategy, predicate);
    }
}