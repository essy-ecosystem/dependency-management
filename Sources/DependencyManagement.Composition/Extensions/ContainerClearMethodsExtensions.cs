namespace DependencyManagement.Composition.Extensions;

using Components;
using Containers;
using Enums;
using Utils;

public static class ContainerClearMethodsExtensions
{
    public static void Clear<T>(this IContainer container, TraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == TraversalStrategy.Current)
        {
            container.Clear<T>();
            return;
        }

        if (strategy == TraversalStrategy.Initial)
        {
            TraversalService.GetInitial(container).Clear<T>();
            return;
        }

        container.Clear<T>();

        while (container.Father is not null)
        {
            container = container.Father!;
            container.Clear<T>();
        }
    }

    public static void ClearLazy<T>(this IContainer container) where T : class, IComponent
    {
        container.Clear<ILazyComponent<T>>();
    }

    public static void ClearLazy<T>(this IContainer container, TraversalStrategy strategy)
        where T : class, IComponent
    {
        container.Clear<ILazyComponent<T>>(strategy);
    }
}