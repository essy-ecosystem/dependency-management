namespace DependencyManagement.Injection.Extensions;

using Builders;
using Composition.Composites;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class CompositeSetMethodsExtensions
{
    public static ITargetBuilder<T> SetTarget<T>(this IComposite composite) where T : class
    {
        composite.ClearTarget<T>(CompositeTraversalStrategy.Inherit);
        return new TargetBuilder<T>(composite);
    }

    public static void SetTarget<T>(this IComposite composite, ITarget<T> target) where T : class
    {
        composite.Set(target);
    }

    public static void SetTarget<T>(this IComposite composite, ITarget<T> target, CompositeTraversalStrategy strategy)
        where T : class
    {
        composite.Set(target, strategy);
    }

    public static ITargetBuilder<T>? TrySetTarget<T>(this IComposite composite) where T : class
    {
        return !composite.AnyTarget<T>()
            ? new TargetBuilder<T>(composite)
            : null;
    }

    public static ITargetBuilder<T>? TrySetTarget<T>(this IComposite composite, CompositeTraversalStrategy strategy)
        where T : class
    {
        return !composite.AnyTarget<T>(strategy)
            ? new TargetBuilder<T>(composite)
            : null;
    }

    public static bool TrySetTarget<T>(this IComposite composite, ITarget<T> target) where T : class
    {
        return composite.TrySet(target);
    }

    public static bool TrySetTarget<T>(this IComposite composite, ITarget<T> target,
        CompositeTraversalStrategy strategy) where T : class
    {
        return composite.TrySet(target, strategy);
    }
}