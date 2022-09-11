namespace DependencyManagement.Composition.Extensions;

using Components;
using Containers;
using Enums;
using Utils;

public static class ContainerRemoveMethodsExtensions
{
    public static bool Remove<T>(this IContainer container, T component, TraversalStrategy strategy)
        where T : class, IComponent
    {
        if (strategy == TraversalStrategy.Current) return container.Remove(component);
        if (strategy == TraversalStrategy.Initial)
            return TraversalService.GetInitial(container).Remove(component);

        if (container.Remove(component)) return true;

        while (container.Father is not null)
        {
            container = container.Father!;
            if (container.Remove(component)) return true;
        }

        return false;
    }

    public static bool Remove<T>(this IContainer container, Predicate<T> predicate) where T : class, IComponent
    {
        var components = container.Where(predicate);
        var result = false;

        foreach (var component in components)
        {
            if (container.Remove(component))
            {
                result = true;
            }
        }

        return result;
    }

    public static bool Remove<T>(this IContainer container, TraversalStrategy strategy, Predicate<T> predicate)
        where T : class, IComponent
    {
        if (strategy == TraversalStrategy.Current) return container.Remove(predicate);
        if (strategy == TraversalStrategy.Initial)
            return TraversalService.GetInitial(container).Remove(predicate);

        var result = container.Remove(predicate);

        while (container.Father is not null)
        {
            container = container.Father!;
            if (container.Remove(predicate)) result = true;
        }

        return result;
    }

    public static bool RemoveLazy<T>(this IContainer container, ILazyComponent<T> component) where T : class, IComponent
    {
        return container.Remove(component);
    }

    public static bool RemoveLazy<T>(this IContainer container, ILazyComponent<T> component,
        TraversalStrategy strategy) where T : class, IComponent
    {
        return container.Remove(component, strategy);
    }

    public static bool RemoveLazy<T>(this IContainer container, T component) where T : class, IComponent
    {
        return container.RemoveLazy(container
            .FirstLazy<T>(lazy => lazy.IsValueCreated && lazy.Value == component));
    }

    public static bool RemoveLazy<T>(this IContainer container, T component, TraversalStrategy strategy)
        where T : class, IComponent
    {
        return container.RemoveLazy(container
            .FirstLazy<T>(strategy, lazy => lazy.IsValueCreated && lazy.Value == component), strategy);
    }

    public static bool RemoveLazy<T>(this IContainer container, Predicate<ILazyComponent<T>> predicate)
        where T : class, IComponent
    {
        return container.Remove(predicate);
    }

    public static bool RemoveLazy<T>(this IContainer container, TraversalStrategy strategy,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return container.Remove(strategy, predicate);
    }
}