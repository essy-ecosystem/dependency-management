namespace DependencyManagement.Composition.Extensions;

using Components;
using Composites;
using Enums;
using Utils;

public static class CompositeAnyMethodsExtensions
{
    public static bool Any<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current) return composite.Any<T>();
        if (strategy == CompositeTraversalStrategy.Initial) return CompositeTreeUtils.GetLast(composite).Any<T>();

        if (composite.Any<T>()) return true;

        while (composite.Father is not null)
        {
            composite = composite.Father!;
            if (composite.Any<T>()) return true;
        }

        return false;
    }

    public static bool AnyLazy<T>(this IReadOnlyComposite composite) where T : class, IComponent
    {
        return composite.Any<ILazyComponent<T>>();
    }

    public static bool AnyLazy<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return composite.Any<ILazyComponent<T>>(strategy);
    }
}