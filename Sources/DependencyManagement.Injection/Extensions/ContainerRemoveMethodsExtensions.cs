namespace DependencyManagement.Injection.Extensions;

using Composition.Components;
using Composition.Containers;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class ContainerRemoveMethodsExtensions
{
    public static bool RemoveTarget<T>(this IContainer container, ITarget<T> component) where T : class
    {
        return container.Remove(component);
    }

    public static bool RemoveTarget<T>(this IContainer container, ITarget<T> component,
        TraversalStrategy strategy) where T : class
    {
        return container.Remove(component, strategy);
    }

    public static bool RemoveTarget<T>(this IContainer container, Predicate<T> predicate) where T : class, IComponent
    {
        return container.Remove(predicate);
    }

    public static bool RemoveTarget<T>(this IContainer container, TraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        return container.Remove(strategy, predicate);
    }
}