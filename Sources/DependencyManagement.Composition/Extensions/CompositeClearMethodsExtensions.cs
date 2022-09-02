namespace DependencyManagement.Composition.Extensions;

using Components;
using Containers;
using Enums;
using Utils;

public static class CompositeClearMethodsExtensions
{
    public static void Clear<T>(this IContainer container, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current)
        {
            container.Clear<T>();
            return;
        }

        if (strategy == CompositeTraversalStrategy.Initial)
        {
            ContainerTreeUtils.GetLast(container).Clear<T>();
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

    public static void ClearLazy<T>(this IContainer container, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        container.Clear<ILazyComponent<T>>(strategy);
    }
}