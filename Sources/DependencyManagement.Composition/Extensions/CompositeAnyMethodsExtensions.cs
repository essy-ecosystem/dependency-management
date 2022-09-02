namespace DependencyManagement.Composition.Extensions;

using Components;
using Containers;
using Enums;
using Utils;

public static class CompositeAnyMethodsExtensions
{
    public static bool Any<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current) return container.Any<T>();
        if (strategy == CompositeTraversalStrategy.Initial) return ContainerTreeUtils.GetLast(container).Any<T>();

        if (container.Any<T>()) return true;

        while (container.Father is not null)
        {
            container = container.Father!;
            if (container.Any<T>()) return true;
        }

        return false;
    }

    public static bool AnyLazy<T>(this IReadOnlyContainer container) where T : class, IComponent
    {
        return container.Any<ILazyComponent<T>>();
    }

    public static bool AnyLazy<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return container.Any<ILazyComponent<T>>(strategy);
    }
}