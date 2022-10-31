namespace DependencyManagement.Linq;

using System.Runtime.CompilerServices;
using Components;
using Containers;

public static class Containable
{
    public static bool Contains<T>(this IReadOnlyContainer container, Predicate<T> filter) where T : IComponent
    {
        var components = container.All<T>();

        return components.Any(component => filter(component));
    }
    
    public static bool Contains<T>(this IReadOnlyContainer container, bool inherit, Predicate<T> filter) where T : IComponent
    {
        if (inherit is false) return container.Contains(filter);
        
        var castedContainer = Unsafe.As<IReadOnlyContainer>(container);
        
        do
        {
            if (castedContainer.Contains(filter)) return true;
            castedContainer = castedContainer.Father;
        } while (castedContainer is not null);

        return false;
    }
}