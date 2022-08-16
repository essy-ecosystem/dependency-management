using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;
using DependencyManagement.Composition.Enums;

namespace DependencyManagement.Composition.Extensions;

public static class CompositeAnyMethodsExtensions
{
    public static bool Any<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (composite.Any<T>()) return true;
        if (strategy == CompositeTraversalStrategy.Current) return false;

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