namespace DependencyManagement.Composition.Extensions;

using Components;
using Containers;
using Enums;
using Utils;

public static class CompositeAllMethodsExtensions
{
    public static IReadOnlyList<T> All<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current || container.Father is null) return container.All<T>();
        if (strategy == CompositeTraversalStrategy.Initial) return ContainerTreeUtils.GetLast(container).All<T>();

        var components = new List<T>(container.All<T>());

        do
        {
            container = container.Father!;
            components.AddRange(container.All<T>());
        } while (container.Father is not null);

        return components;
    }

    public static IReadOnlyList<ILazyComponent<T>> AllLazy<T>(this IReadOnlyContainer container)
        where T : class, IComponent
    {
        return container.All<ILazyComponent<T>>();
    }

    public static IReadOnlyList<ILazyComponent<T>> AllLazy<T>(this IReadOnlyContainer container,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        return container.All<ILazyComponent<T>>(strategy);
    }
}