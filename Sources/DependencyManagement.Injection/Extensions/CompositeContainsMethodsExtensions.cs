namespace DependencyManagement.Injection.Extensions;

using Composition.Composites;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class CompositeContainsMethodsExtensions
{
    public static bool ContainsTarget<T>(this IReadOnlyComposite composite, ITarget<T> component) where T : class
    {
        return composite.Contains(component);
    }

    public static bool ContainsTarget<T>(this IReadOnlyComposite composite, ITarget<T> component,
        CompositeTraversalStrategy strategy) where T : class
    {
        return composite.Contains(component, strategy);
    }

    public static bool ContainsTarget<T>(this IReadOnlyComposite composite, Predicate<ITarget<T>> predicate)
        where T : class
    {
        return composite.Contains(predicate);
    }

    public static bool ContainsTarget<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return composite.Contains(strategy, predicate);
    }
}