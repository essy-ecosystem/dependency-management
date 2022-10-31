namespace DependencyManagement.Linq;

using Components;
using Containers;

public static class Setable
{
    public static void Set<T>(this IContainer container, T component) where T : IComponent
    {
        container.Clear<T>();
        container.Add(component);
    }
    
    public static void Set<T>(this IContainer container, T component, bool inherit) where T : IComponent
    {
        container.Clear<T>(inherit);
        container.Add(component);
    }
    
    public static bool TrySet<T>(this IContainer container, T component) where T : IComponent
    {
        if (container.Any<T>()) return false;
        container.Add(component);
        return true;
    }
    
    public static bool TrySet<T>(this IContainer container, T component, bool inherit) where T : IComponent
    {
        if (container.Any<T>(inherit)) return false;
        container.Add(component);
        return true;
    }
}