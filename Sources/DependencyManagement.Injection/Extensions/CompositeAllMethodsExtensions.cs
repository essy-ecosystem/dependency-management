namespace DependencyManagement.Injection.Extensions;

using Collections;
using Composition.Containers;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class CompositeAllMethodsExtensions
{
    public static IReadOnlyList<ITarget<T>> AllTarget<T>(this IReadOnlyContainer container) where T : class
    {
        return container.All<ITarget<T>>();
    }

    public static IReadOnlyList<ITarget<T>> AllTarget<T>(this IReadOnlyContainer container,
        CompositeTraversalStrategy strategy) where T : class
    {
        return container.All<ITarget<T>>(strategy);
    }

    public static IReadOnlyList<T> AllInstance<T>(this IReadOnlyContainer container) where T : class
    {
        return new ReadOnlyTargetList<T>(container, container.All<ITarget<T>>());
    }

    public static IReadOnlyList<T> AllInstance<T>(this IReadOnlyContainer container,
        CompositeTraversalStrategy strategy) where T : class
    {
        return new ReadOnlyTargetList<T>(container, container.All<ITarget<T>>(strategy));
    }
}