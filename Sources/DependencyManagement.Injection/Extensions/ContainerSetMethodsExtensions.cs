namespace DependencyManagement.Injection.Extensions;

using Builders;
using Composition.Containers;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class ContainerSetMethodsExtensions
{
    public static ITargetBuilder<T> SetTarget<T>(this IContainer container) where T : class
    {
        return new SettableTargetBuilder<T>(container);
    }

    public static void SetTarget<T>(this IContainer container, ITarget<T> target) where T : class
    {
        container.Set(target);
    }

    public static void SetTarget<T>(this IContainer container, ITarget<T> target, TraversalStrategy strategy)
        where T : class
    {
        container.Set(target, strategy);
    }

    public static ITargetBuilder<T>? TrySetTarget<T>(this IContainer container) where T : class
    {
        return !container.AnyTarget<T>(TraversalStrategy.Inherit)
            ? new SettableTargetBuilder<T>(container)
            : null;
    }

    public static bool TrySetTarget<T>(this IContainer container, ITarget<T> target) where T : class
    {
        return container.TrySet(target);
    }

    public static bool TrySetTarget<T>(this IContainer container, ITarget<T> target,
        TraversalStrategy strategy) where T : class
    {
        return container.TrySet(target, strategy);
    }
}