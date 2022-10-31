namespace DependencyManagement.Linq;

using System.Runtime.CompilerServices;
using Components;
using Containers;

public static class Firstable
{
    public static bool TryFirst<T>(this IReadOnlyContainer container, out T? component) where T : IComponent
    {
        var components = container.All<T>();
        
        var result = components.Count != 0;
        
        component = result 
            ? components[0]
            : default;
        
        return result;
    }
    
    public static bool TryFirst<T>(this IReadOnlyContainer container, out T? component, Predicate<T> filter) where T : IComponent
    {
        var components = container.All<T>();
        
        foreach (var c in components)
        {
            if (filter(c) is false) continue;
            component = c;
            return true;
        }
        
        component = default;
        return false;
    }
    
    public static bool TryFirst<T>(this IReadOnlyContainer container, out T? component, bool inherit) where T : IComponent
    {
        if (inherit is false) return container.TryFirst(out component);

        var containers = new List<IReadOnlyContainer>();
        var castedContainer = Unsafe.As<IReadOnlyContainer>(container);
        
        do
        {
            containers.Add(castedContainer);
            castedContainer = castedContainer.Father;
        } while (castedContainer is not null);
        
        for (var i = containers.Count - 1; i >= 0; i--)
        {
            if (containers[i].TryFirst(out component)) return true;
        }

        component = default;
        return false;
    }
    
    public static bool TryFirst<T>(this IReadOnlyContainer container, out T? component, bool inherit, Predicate<T> filter) where T : IComponent
    {
        if (inherit is false) return container.TryFirst(out component);

        var containers = new List<IReadOnlyContainer>();
        var castedContainer = Unsafe.As<IReadOnlyContainer>(container);
        
        do
        {
            containers.Add(castedContainer);
            castedContainer = castedContainer.Father;
        } while (castedContainer is not null);
        
        for (var i = containers.Count - 1; i >= 0; i--)
        {
            if (containers[i].TryFirst(out component, filter)) return true;
        }

        component = default;
        return false;
    }
    
    public static T? TryFirst<T>(this IReadOnlyContainer container) where T : IComponent
    {
        container.TryFirst<T>(out var component);
        return component;
    }
    
    public static T? TryFirst<T>(this IReadOnlyContainer container, Predicate<T> filter) where T : IComponent
    {
        container.TryFirst(out var component, filter);
        return component;
    }
    
    public static T? TryFirst<T>(this IReadOnlyContainer container, bool inherit) where T : IComponent
    {
        container.TryFirst<T>(out var component, inherit);
        return component;
    }
    
    public static T? TryFirst<T>(this IReadOnlyContainer container, bool inherit, Predicate<T> filter) where T : IComponent
    {
        container.TryFirst(out var component, inherit, filter);
        return component;
    }
    
    public static T First<T>(this IReadOnlyContainer container) where T : IComponent
    {
        return container.TryFirst<T>()
            ?? throw new InvalidOperationException(
                $"No component of type {typeof(T).Name} found in container.");
    }
    
    public static T First<T>(this IReadOnlyContainer container, Predicate<T> filter) where T : IComponent
    {
        return container.TryFirst(filter)
            ?? throw new InvalidOperationException(
                $"No component of type {typeof(T).Name} found in container.");
    }
    
    public static T First<T>(this IReadOnlyContainer container, bool inherit) where T : IComponent
    {
        return container.TryFirst<T>(inherit)
            ?? throw new InvalidOperationException(
                $"No component of type {typeof(T).Name} found in container.");
    }
    
    public static T First<T>(this IReadOnlyContainer container, bool inherit, Predicate<T> filter) where T : IComponent
    {
        return container.TryFirst(inherit, filter)
            ?? throw new InvalidOperationException(
                $"No component of type {typeof(T).Name} found in container.");
    }
}