namespace DependencyManagement.Linq;

using System.Runtime.CompilerServices;
using Components;
using Containers;

public static class Lastable
{
    public static bool TryLast<T>(this IReadOnlyContainer container, out T? component) where T : IComponent
    {
        var components = container.All<T>();
        
        var result = components.Count != 0;
        
        component = result
            ? components[^1]
            : default;
        
        return result;
    }
    
    public static bool TryLast<T>(this IReadOnlyContainer container, out T? component, Predicate<T> filter) where T : IComponent
    {
        var components = container.All<T>();
        
        for (var i = components.Count - 1; i >= 0; i--)
        {
            var current = components[i];
            if (filter(current) is false) continue;
            component = current;
            return true;
        }
        
        component = default;
        return false;
    }
    
    public static bool TryLast<T>(this IReadOnlyContainer container, out T? component, bool inherit) where T : IComponent
    {
        if (inherit is false) return container.TryLast(out component);
        
        var castedContainer = Unsafe.As<IReadOnlyContainer>(container);
        
        do
        {
            if (castedContainer.TryLast(out component)) return true;
            castedContainer = castedContainer.Father;
        } while (castedContainer is not null);

        return false;
    }
    
    public static bool TryLast<T>(this IReadOnlyContainer container, out T? component, bool inherit, Predicate<T> filter) where T : IComponent
    {
        if (inherit is false) return container.TryFirst(out component);

        var castedContainer = Unsafe.As<IReadOnlyContainer>(container);
        
        do
        {
            if (castedContainer.TryLast(out component, filter)) return true;
            castedContainer = castedContainer.Father;
        } while (castedContainer is not null);

        return false;
    }
    
    public static T? TryLast<T>(this IReadOnlyContainer container) where T : IComponent
    {
        container.TryLast<T>(out var component);
        return component;
    }
    
    public static T? TryLast<T>(this IReadOnlyContainer container, Predicate<T> filter) where T : IComponent
    {
        container.TryLast(out var component, filter);
        return component;
    }
    
    public static T? TryLast<T>(this IReadOnlyContainer container, bool inherit) where T : IComponent
    {
        container.TryLast<T>(out var component, inherit);
        return component;
    }
    
    public static T? TryLast<T>(this IReadOnlyContainer container, bool inherit, Predicate<T> filter) where T : IComponent
    {
        container.TryLast(out var component, inherit, filter);
        return component;
    }
    
    public static T Last<T>(this IReadOnlyContainer container) where T : IComponent
    {
        return container.TryLast<T>()
            ?? throw new InvalidOperationException(
                $"No component of type {typeof(T).Name} found in container.");
    }
    
    public static T Last<T>(this IReadOnlyContainer container, Predicate<T> filter) where T : IComponent
    {
        return container.TryLast(filter)
            ?? throw new InvalidOperationException(
                $"No component of type {typeof(T).Name} found in container.");
    }
    
    public static T Last<T>(this IReadOnlyContainer container, bool inherit) where T : IComponent
    {
        return container.TryLast<T>(inherit)
            ?? throw new InvalidOperationException(
                $"No component of type {typeof(T).Name} found in container.");
    }
    
    public static T Last<T>(this IReadOnlyContainer container, bool inherit, Predicate<T> filter) where T : IComponent
    {
        return container.TryLast(inherit, filter)
            ?? throw new InvalidOperationException(
                $"No component of type {typeof(T).Name} found in container.");
    }
}