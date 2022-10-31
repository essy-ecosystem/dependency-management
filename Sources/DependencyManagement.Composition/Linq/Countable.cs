namespace DependencyManagement.Linq;

using Components;
using Containers;

public static class Countable
{
    public static int Count<T>(this IReadOnlyContainer container) where T : IComponent
    {
        return container.All<T>().Count;
    }
    
    public static int Count<T>(this IReadOnlyContainer container, bool inherit) where T : IComponent
    {
        return container.All<T>(inherit).Count;
    }
    
    public static int Count<T>(this IReadOnlyContainer container, Predicate<T> filter) where T : IComponent
    {
        return container.Where(filter).Count;
    }
    
    public static int Count<T>(this IReadOnlyContainer container, bool inherit, Predicate<T> filter) where T : IComponent
    {
        return container.Where(inherit, filter).Count;
    }
}