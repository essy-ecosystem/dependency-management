namespace DependencyManagement.Injection.Extensions;

using Builders;
using Composition.Containers;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class CompositeAddMethodsExtensions
{
    public static ITargetBuilder<T> AddTarget<T>(this IContainer container) where T : class
    {
        return new TargetBuilder<T>(container);
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
        CompositeTraversalStrategy strategy) where T : class
    {
        return container.TryAdd(target, strategy);
    }
}