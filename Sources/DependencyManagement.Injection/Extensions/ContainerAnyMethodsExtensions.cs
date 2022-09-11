namespace DependencyManagement.Injection.Extensions;

using Composition.Containers;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class ContainerAnyMethodsExtensions
{
    public static bool AnyTarget<T>(this IReadOnlyContainer container) where T : class
    {
        return container.Any<ITarget<T>>();
    }

    public static bool AnyTarget<T>(this IReadOnlyContainer container, TraversalStrategy strategy)
        where T : class
    {
        return container.Any<ITarget<T>>(strategy);
    }

    public static bool AnyInstance<T>(this IReadOnlyContainer container) where T : class
    {
        return container.AnyTarget<T>();
    }

    public static bool AnyInstance<T>(this IReadOnlyContainer container, TraversalStrategy strategy)
        where T : class
    {
        return container.AnyTarget<T>(strategy);
    }
}