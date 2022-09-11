namespace DependencyManagement.Composition.Extensions;

using Components;
using Containers;
using Enums;
using Utils;

public static class ContainerAllMethodsExtensions
{
    public static IReadOnlyList<T> All<T>(this IReadOnlyContainer container, TraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == TraversalStrategy.Current || container.Father is null) return container.All<T>();
        if (strategy == TraversalStrategy.Initial) return TraversalService.GetInitial(container).All<T>();

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
        TraversalStrategy strategy) where T : class, IComponent
    {
        return container.All<ILazyComponent<T>>(strategy);
    }
}