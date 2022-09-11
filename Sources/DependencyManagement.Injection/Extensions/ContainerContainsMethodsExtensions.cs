namespace DependencyManagement.Injection.Extensions;

using Composition.Containers;
using Composition.Enums;
using Composition.Extensions;
using Composition.Utils;
using Targets;

public static class ContainerContainsMethodsExtensions
{
    public static bool ContainsTarget<T>(this IReadOnlyContainer container, ITarget<T> component) where T : class
    {
        return container.Contains(component);
    }

    public static bool ContainsTarget<T>(this IReadOnlyContainer container, ITarget<T> component,
        TraversalStrategy strategy) where T : class
    {
        return container.Contains(component, strategy);
    }

    public static bool ContainsTarget<T>(this IReadOnlyContainer container, Predicate<ITarget<T>> predicate)
        where T : class
    {
        return container.Contains(predicate);
    }

    public static bool ContainsTarget<T>(this IReadOnlyContainer container, TraversalStrategy strategy,
        Predicate<ITarget<T>> predicate) where T : class
    {
        return container.Contains(strategy, predicate);
    }
    
    public static bool ContainsInstance<T>(this IReadOnlyContainer container, T instance) where T : class
    {
        return container.AllTarget<T>().Any(target => target.IsInstanceCached(instance));
    }
    
    public static bool ContainsInstance<T>(this IReadOnlyContainer container, T instance, 
        TraversalStrategy strategy) where T : class
    {
        if (strategy == TraversalStrategy.Initial)
        {
            return TraversalService.GetInitial(container).ContainsInstance(instance);
        }

        var currentContainer = container;

        if (currentContainer.ContainsInstance(container)) return true;
        
        while (currentContainer.Father is not null)
        {
            currentContainer = currentContainer.Father!;
            
            if (currentContainer.ContainsInstance(container)) return true;
        }

        return false;
    }
    public static bool ContainsInstance<T>(this IReadOnlyContainer container, Predicate<T> instance) where T : class
    {
        return container.AllTarget<T>().Any(target => instance(target.ProvideInstance(container)));
    }
    
    public static bool ContainsInstance<T>(this IReadOnlyContainer container, Predicate<T> instance, 
        TraversalStrategy strategy) where T : class
    {
        if (strategy == TraversalStrategy.Initial)
        {
            return TraversalService.GetInitial(container).ContainsInstance(instance);
        }

        var currentContainer = container;

        if (currentContainer.ContainsInstance(container)) return true;
        
        while (currentContainer.Father is not null)
        {
            currentContainer = currentContainer.Father!;
            
            if (currentContainer.ContainsInstance(container)) return true;
        }

        return false;
    }
}