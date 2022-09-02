namespace DependencyManagement.Composition.Extensions;

using Components;
using Composites;
using Enums;
using Utils;

public static class CompositeClearMethodsExtensions
{
    public static void Clear<T>(this IComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current)
        {
            composite.Clear<T>();
            return;
        }

        if (strategy == CompositeTraversalStrategy.Initial)
        {
            CompositeTreeUtils.GetLast(composite).Clear<T>();
            return;
        }
        
        composite.Clear<T>();

        while (composite.Father is not null)
        {
            composite = composite.Father!;
            composite.Clear<T>();
        }
    }

    public static void ClearLazy<T>(this IComposite composite) where T : class, IComponent
    {
        composite.Clear<ILazyComponent<T>>();
    }

    public static void ClearLazy<T>(this IComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        composite.Clear<ILazyComponent<T>>(strategy);
    }
}