using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;
using DependencyManagement.Composition.Enums;

namespace DependencyManagement.Composition.Extensions;

public static class CompositeCountMethodsExtensions
{
    public static int Count<T>(this IReadOnlyComposite composite) where T : class, IComponent
    {
        return composite.All<T>().Count;
    }

    public static int Count<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return composite.All<T>(strategy).Count;
    }

    public static int Count<T>(this IReadOnlyComposite composite, Predicate<T> predicate) where T : class, IComponent
    {
        return composite.Where(predicate).Count;
    }

    public static int Count<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        return composite.Where(strategy, predicate).Count;
    }

    public static int CountLazy<T>(this IReadOnlyComposite composite) where T : class, IComponent
    {
        return composite.Count<ILazyComponent<T>>();
    }

    public static int CountLazy<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return composite.Count<ILazyComponent<T>>(strategy);
    }

    public static int CountLazy<T>(this IReadOnlyComposite composite, Predicate<ILazyComponent<T>> predicate)
        where T : class, IComponent
    {
        return composite.Count(predicate);
    }

    public static int CountLazy<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return composite.Count(strategy, predicate);
    }
}