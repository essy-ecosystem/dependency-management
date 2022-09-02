namespace DependencyManagement.Composition.Extensions;

using Components;
using Composites;
using Enums;
using Utils;

public static class CompositeAllMethodsExtensions
{
    public static IReadOnlyList<T> All<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current || composite.Father is null) return composite.All<T>();
        if (strategy == CompositeTraversalStrategy.Initial) return CompositeTreeUtils.GetLast(composite).All<T>();

        var components = new List<T>(composite.All<T>());

        do
        {
            composite = composite.Father!;
            components.AddRange(composite.All<T>());
        } while (composite.Father is not null);

        return components;
    }

    public static IReadOnlyList<ILazyComponent<T>> AllLazy<T>(this IReadOnlyComposite composite)
        where T : class, IComponent
    {
        return composite.All<ILazyComponent<T>>();
    }

    public static IReadOnlyList<ILazyComponent<T>> AllLazy<T>(this IReadOnlyComposite composite,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        return composite.All<ILazyComponent<T>>(strategy);
    }
}