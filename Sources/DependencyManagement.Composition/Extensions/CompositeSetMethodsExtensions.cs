namespace DependencyManagement.Composition.Extensions;

using Components;
using Containers;
using Enums;

public static class CompositeSetMethodsExtensions
{
    public static void Set<T>(this IContainer container, T component) where T : class, IComponent
    {
        container.Set(component, CompositeTraversalStrategy.Current);
    }

    public static void Set<T>(this IContainer container, T component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        container.Clear<T>(strategy);
        container.Add(component);
    }

    public static bool TrySet<T>(this IContainer container, T component) where T : class, IComponent
    {
        return container.TrySet(component, CompositeTraversalStrategy.Current);
    }

    public static bool TrySet<T>(this IContainer container, T component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        var any = container.Any<T>();
        if (!any) container.Add(component);
        return any;
    }

    public static void SetLazy<T>(this IContainer container, ILazyComponent<T> component) where T : class, IComponent
    {
        container.Set(component);
    }

    public static void SetLazy<T>(this IContainer container, ILazyComponent<T> component,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        container.Set(component, strategy);
    }

    public static void SetLazy<T>(this IContainer container, Func<T> component) where T : class, IComponent
    {
        container.SetLazy(new LazyComponent<T>(component));
    }

    public static void SetLazy<T>(this IContainer container, Func<T> component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        container.SetLazy(new LazyComponent<T>(component), strategy);
    }

    public static bool TrySetLazy<T>(this IContainer container, ILazyComponent<T> component) where T : class, IComponent
    {
        return container.TrySet(component);
    }

    public static bool TrySetLazy<T>(this IContainer container, ILazyComponent<T> component,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        return container.TrySet(component, strategy);
    }

    public static bool TrySetLazy<T>(this IContainer container, Func<T> component) where T : class, IComponent
    {
        return container.TrySetLazy(new LazyComponent<T>(component));
    }

    public static bool TrySetLazy<T>(this IContainer container, Func<T> component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        return container.TrySetLazy(new LazyComponent<T>(component), strategy);
    }
}