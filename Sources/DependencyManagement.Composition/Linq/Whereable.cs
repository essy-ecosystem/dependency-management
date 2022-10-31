namespace DependencyManagement.Linq;

using System.Runtime.CompilerServices;
using Components;
using Containers;

public static class Whereable
{
    public static IReadOnlyList<T> Where<T>(this IReadOnlyContainer container, Predicate<T> filter) where T : IComponent
    {
        var all = container.All<T>();

        return all.Where(component => filter(component)).ToList();
    }
    
    public static IReadOnlyList<T> Where<T>(this IReadOnlyContainer container, bool inherit, Predicate<T> filter) where T : IComponent
    {
        if (inherit is false) container.Where(filter);

        var components = new List<T>();
        var castedContainer = Unsafe.As<IReadOnlyContainer>(container);
        
        do
        {
            var currentComponents = castedContainer.Where(filter);
            components.InsertRange(0, currentComponents);
            castedContainer = castedContainer.Father;
        } while (castedContainer is not null);

        return components;
    }
}