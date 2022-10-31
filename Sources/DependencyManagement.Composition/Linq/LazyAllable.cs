namespace DependencyManagement.Linq;

using Components;
using Containers;

/// <summary>
/// Lazy allable LINQ methods for <see cref="DependencyManagement.Containers.IContainer"/>.
/// </summary>
public static class LazyAllable
{
    /// <summary>
    /// Gets the <see cref="IComponent"/> as lazy component read only list
    /// with the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the components.
    /// </typeparam>
    /// <param name="container">
    /// The container.
    /// </param>
    /// <returns>
    /// The <see cref="IComponent"/> as lazy component read only list.
    /// </returns>
    public static IReadOnlyList<ILazyComponent<T>> AllLazy<T>(this IReadOnlyContainer container) where T : IComponent
    {
        return container.All<ILazyComponent<T>>();
    }
    
    /// <summary>
    /// Gets the <see cref="IComponent"/> as lazy component read only list
    /// with the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the components.
    /// </typeparam>
    /// <param name="container">
    /// The container.
    /// </param>
    /// <param name="inherit">
    /// If true, the components of the fathers container will be included.
    /// </param>
    /// <returns>
    /// The <see cref="IComponent"/> as lazy component read only list.
    /// </returns>
    public static IReadOnlyList<ILazyComponent<T>> AllLazy<T>(this IReadOnlyContainer container, bool inherit) where T : IComponent
    {
        return container.All<ILazyComponent<T>>(inherit);
    }
}