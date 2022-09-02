namespace DependencyManagement.Composition.Extensions;

using Components;
using Composites;
using Enums;

public static class CompositeSetMethodsExtensions
{
    public static void Set<T>(this IComposite composite, T component) where T : class, IComponent
    {
        composite.Set(component, CompositeTraversalStrategy.Current);
    }

    public static void Set<T>(this IComposite composite, T component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        composite.Clear<T>(strategy);
        composite.Add(component);
    }

    public static bool TrySet<T>(this IComposite composite, T component) where T : class, IComponent
    {
        return composite.TrySet(component, CompositeTraversalStrategy.Current);
    }

    public static bool TrySet<T>(this IComposite composite, T component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        var any = composite.Any<T>();
        if (!any) composite.Add(component);
        return any;
    }

    public static void SetLazy<T>(this IComposite composite, ILazyComponent<T> component) where T : class, IComponent
    {
        composite.Set(component);
    }

    public static void SetLazy<T>(this IComposite composite, ILazyComponent<T> component,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        composite.Set(component, strategy);
    }

    public static void SetLazy<T>(this IComposite composite, Func<T> component) where T : class, IComponent
    {
        composite.SetLazy(new LazyComponent<T>(component));
    }

    public static void SetLazy<T>(this IComposite composite, Func<T> component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        composite.SetLazy(new LazyComponent<T>(component), strategy);
    }

    public static bool TrySetLazy<T>(this IComposite composite, ILazyComponent<T> component) where T : class, IComponent
    {
        return composite.TrySet(component);
    }

    public static bool TrySetLazy<T>(this IComposite composite, ILazyComponent<T> component,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        return composite.TrySet(component, strategy);
    }

    public static bool TrySetLazy<T>(this IComposite composite, Func<T> component) where T : class, IComponent
    {
        return composite.TrySetLazy(new LazyComponent<T>(component));
    }

    public static bool TrySetLazy<T>(this IComposite composite, Func<T> component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return composite.TrySetLazy(new LazyComponent<T>(component), strategy);
    }
}