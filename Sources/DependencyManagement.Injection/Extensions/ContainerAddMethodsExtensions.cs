namespace DependencyManagement.Injection.Extensions;

using Builders;
using Composition.Containers;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class ContainerAddMethodsExtensions
{
    public static ITargetBuilder<T> AddTarget<T>(this IContainer container) where T : class
    {
        return new AddableTargetBuilder<T>(container);
    }

    public static void AddTarget<T>(this IContainer container, ITarget<T> target) where T : class
    {
        container.Add(target);
    }

    public static bool TryAddTarget<T>(this IContainer container, ITarget<T> target) where T : class
    {
        return container.TryAdd(target);
    }

    public static bool TryAddTarget<T>(this IContainer container, ITarget<T> target,
        TraversalStrategy strategy) where T : class
    {
        return container.TryAdd(target, strategy);
    }
}