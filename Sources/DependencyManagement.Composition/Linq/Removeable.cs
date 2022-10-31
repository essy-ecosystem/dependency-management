namespace DependencyManagement.Linq;

using System.Runtime.CompilerServices;
using Components;
using Containers;

public static class Removeable
{
    public static bool Remove<T>(this IContainer container, Predicate<T> filter) where T : IComponent
    {
        var components = container.Where(filter);
        var result = false;

        foreach (var component in components)
        {
            if (container.Remove(component) is false) continue;
            result = true;
        }

        return result;
    }
    
    public static bool Remove<T>(this IContainer container, bool inherit, Predicate<T> filter) where T : IComponent
    {
        if (inherit is false) return container.Remove(filter);
        
        var castedContainer = Unsafe.As<IContainer>(container);
        var result = false;
        
        do
        {
            if (castedContainer.Remove(filter)) result = true;
            castedContainer = castedContainer.Father;
        } while (castedContainer is not null);

        return result;
    }
}