using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;
using DependencyManagement.Composition.Enums;

namespace DependencyManagement.Composition.Extensions;

public static class CompositeAddMethodsExtensions
{
    public static void AddLazy<T>(this IComposite composite, ILazyComponent<T> component) where T : class, IComponent
    {
        composite.Add(component);
    }

    public static void AddLazy<T>(this IComposite composite, Func<T> component) where T : class, IComponent
    {
        composite.Add<ILazyComponent<T>>(new LazyComponent<T>(component));
    }

    public static bool TryAdd<T>(this IComposite composite, T component) where T : class, IComponent
    {
        return composite.TryAdd(component, CompositeTraversalStrategy.Current);
    }

    public static bool TryAdd<T>(this IComposite composite, T component, CompositeTraversalStrategy strategy)
        where T : class, IComponent
    {
        if (composite.Contains(component, strategy)) return false;
        composite.Add(component);
        return true;
    }

    public static bool TryAddLazy<T>(this IComposite composite, ILazyComponent<T> component) where T : class, IComponent
    {
        return composite.TryAdd(component);
    }

    public static bool TryAddLazy<T>(this IComposite composite, ILazyComponent<T> component,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        return composite.TryAdd(component, strategy);
    }

    public static bool TryAddLazy<T>(this IComposite composite, Func<T> component) where T : class, IComponent
    {
        return composite.TryAddLazy(new LazyComponent<T>(component));
    }

    public static bool TryAddLazy<T>(this IComposite composite, Func<T> component,
        CompositeTraversalStrategy strategy) where T : class, IComponent
    {
        return composite.TryAddLazy(new LazyComponent<T>(component), strategy);
    }
}