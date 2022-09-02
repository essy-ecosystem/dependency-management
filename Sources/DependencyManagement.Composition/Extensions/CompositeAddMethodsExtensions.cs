namespace DependencyManagement.Composition.Extensions;

using Components;
using Containers;
using Enums;

public static class CompositeAddMethodsExtensions
{
    public static void AddLazy<T>(this IContainer container, ILazyComponent<T> component) where T : class, IComponent
    {
        container.Add(component);
    }

    public static void AddLazy<T>(this IContainer container, Func<T> component) where T : class, IComponent
    {
        container.Add<ILazyComponent<T>>(new LazyComponent<T>(component));
    }

    public static bool TryAdd<T>(this IContainer container, T component) where T : class, IComponent
    {
        return container.TryAdd(component, CompositeTraversalStrategy.Current);
    }

    public static bool TryAdd<T>(this IContainer container, T component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (container.Contains(component, strategy)) return false;
        container.Add(component);
        return true;
    }

    public static bool TryAddLazy<T>(this IContainer container, ILazyComponent<T> component) where T : class, IComponent
    {
        return container.TryAdd(component);
    }

    public static bool TryAddLazy<T>(this IContainer container, ILazyComponent<T> component,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        return container.TryAdd(component, strategy);
    }

    public static bool TryAddLazy<T>(this IContainer container, Func<T> component) where T : class, IComponent
    {
        return container.TryAddLazy(new LazyComponent<T>(component));
    }

    public static bool TryAddLazy<T>(this IContainer container, Func<T> component,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        return container.TryAddLazy(new LazyComponent<T>(component), strategy);
    }
}