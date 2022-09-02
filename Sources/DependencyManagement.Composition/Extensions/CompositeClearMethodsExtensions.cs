namespace DependencyManagement.Composition.Extensions;

using Components;
using Composites;
using Enums;

public static class CompositeClearMethodsExtensions
{
    public static void Clear<T>(this IComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        composite.Clear<T>();
        if (strategy == CompositeTraversalStrategy.Current) return;

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