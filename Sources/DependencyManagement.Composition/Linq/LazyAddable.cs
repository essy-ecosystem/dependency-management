namespace DependencyManagement.Linq;

using Components;
using Containers;

/// <summary>
/// Lazy addable LINQ methods for <see cref="DependencyManagement.Containers.IContainer"/>.
/// </summary>
public static class LazyAddable
{
    /// <summary>
    /// Adds a lazy component to the container.
    /// </summary>
    /// <param name="container">
    /// The container.
    /// </param>
    /// <param name="component">
    /// The component.
    /// </param>
    /// <typeparam name="T">
    /// The type of the component.
    /// </typeparam>
    public static void AddLazy<T>(this IContainer container, ILazyComponent<T> component) where T : IComponent
    {
        container.Add(component);
    }
    
    /// <summary>
    /// Adds a lazy component by factory to the container.
    /// </summary>
    /// <param name="container">
    /// The container.
    /// </param>
    /// <param name="component">
    /// The factory of component.
    /// </param>
    /// <typeparam name="T">
    /// The type of the component.
    /// </typeparam>
    public static void AddLazy<T>(this IContainer container, Func<T> component) where T : IComponent
    {
        container.AddLazy(new LazyComponent<T>(component));
    }
    
    /// <summary>
    /// Adds a lazy component to the container.
    /// </summary>
    /// <param name="container">
    /// The container.
    /// </param>
    /// <param name="component">
    /// The component.
    /// </param>
    /// <typeparam name="T">
    /// The type of the component.
    /// </typeparam>
    /// <returns>
    /// True if the component was added, false if the component was already added.
    /// </returns>
    public static bool TryAddLazy<T>(this IContainer container, ILazyComponent<T> component) where T : IComponent
    {
        return container.TryAdd(component);
    }

    /// <summary>
    /// Adds a lazy component to the container.
    /// </summary>
    /// <param name="container">
    /// The container.
    /// </param>
    /// <param name="component">
    /// The component.
    /// </param>
    /// <param name="inherit">
    /// True if the component should be inherited by fathers containers, false otherwise.
    /// </param>
    /// <typeparam name="T">
    /// The type of the component.
    /// </typeparam>
    /// <returns>
    /// True if the component was added, false if the component was already added.
    /// </returns>
    public static bool TryAddLazy<T>(this IContainer container, ILazyComponent<T> component, bool inherit) where T : IComponent
    {
        return container.TryAdd(component, inherit);
    }
    
    /// <summary>
    /// Adds a lazy component by factory to the container.
    /// </summary>
    /// <param name="container">
    /// The container.
    /// </param>
    /// <param name="component">
    /// The factory of component.
    /// </param>
    /// <typeparam name="T">
    /// The type of the component.
    /// </typeparam>
    /// <returns>
    /// True if the component was added, false if the component was already added.
    /// </returns>
    public static bool TryAddLazy<T>(this IContainer container, Func<T> component) where T : IComponent
    {
        return container.TryAddLazy(new LazyComponent<T>(component));
    }

    /// <summary>
    /// Adds a lazy component by factory to the container.
    /// </summary>
    /// <param name="container">
    /// The container.
    /// </param>
    /// <param name="component">
    /// The factory of component.
    /// </param>
    /// <param name="inherit">
    /// True if the component should be inherited by fathers containers, false otherwise.
    /// </param>
    /// <typeparam name="T">
    /// The type of the component.
    /// </typeparam>
    /// <returns>
    /// True if the component was added, false if the component was already added.
    /// </returns>
    public static bool TryAddLazy<T>(this IContainer container, Func<T> component, bool inherit) where T : IComponent
    {
        return container.TryAddLazy(new LazyComponent<T>(component), inherit);
    }
}