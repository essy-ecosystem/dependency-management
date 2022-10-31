namespace DependencyManagement.Linq;

using Components;
using Containers;

/// <summary>
/// Addable LINQ methods for <see cref="DependencyManagement.Containers.IContainer"/>.
/// </summary>
public static class Addable
{
    /// <summary>
    /// Adds the specified <paramref name="component"/> to the container
    /// if it is not already added.
    /// </summary>
    /// <param name="container">
    /// The container.
    /// </param>
    /// <param name="component">
    /// The component.
    /// </param>
    /// <typeparam name="T">
    /// The type of the <paramref name="component"/>.
    /// </typeparam>
    /// <returns>
    /// True if the component was added, false if it was already present.
    /// </returns>
    public static bool TryAdd<T>(this IContainer container, T component) where T : IComponent
    {
        if (container.Contains(component)) return false;
        container.Add(component);
        return true;
    }

    /// <summary>
    /// Adds the specified <paramref name="component"/> to the container
    /// if it is not already added.
    /// </summary>
    /// <param name="container">
    /// The container.
    /// </param>
    /// <param name="component">
    /// The component.
    /// </param>
    /// <param name="inherit">
    /// If true, the component contains (before added) checks in the fathers containers.
    /// </param>
    /// <typeparam name="T">
    /// The type of the <paramref name="component"/>.
    /// </typeparam>
    /// <returns>
    /// True if the component was added, false if it was already present.
    /// </returns>
    public static bool TryAdd<T>(this IContainer container, T component, bool inherit) where T : IComponent
    {
        if (container.Contains(component, inherit)) return false;
        container.Add(component);
        return true;
    }
}