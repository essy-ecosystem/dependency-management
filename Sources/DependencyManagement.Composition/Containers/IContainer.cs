namespace DependencyManagement.Containers;

using Components;

/// <summary>
/// Represents a configurable container that can be used
/// to resolve <see cref="IComponent"/>.
/// </summary>
/// <seealso cref="IReadOnlyContainer"/>
public interface IContainer : IReadOnlyContainer
{
    /// <inheritdoc cref="IReadOnlyContainer.Father"/>
    new IContainer? Father { get; }
    
    /// <summary>
    /// Adds a <paramref name="component"/>
    /// with the specified <typeparamref name="T"/> to the container.
    /// </summary>
    /// <param name="component">
    /// The component to add.
    /// </param>
    /// <typeparam name="T">
    /// The type of the <paramref name="component"/>.
    /// </typeparam>
    void Add<T>(T component) where T : IComponent;
    
    /// <summary>
    /// Removes the <paramref name="component"/>
    /// with the specified <typeparamref name="T"/> from the container.
    /// </summary>
    /// <param name="component">
    /// The component to remove.
    /// </param>
    /// <typeparam name="T">
    /// The type of the <paramref name="component"/>.
    /// </typeparam>
    /// <returns>
    /// True if the <paramref name="component"/> was removed successfully,
    /// false otherwise.
    /// </returns>
    bool Remove<T>(T component) where T : IComponent;

    /// <summary>
    /// Removes the <paramref name="component"/>
    /// with the specified <typeparamref name="T"/> from the container.
    /// </summary>
    /// <param name="component">
    /// The component to remove.
    /// </param>
    /// <param name="inherit">
    /// True if the <paramref name="component"/> should be removed from all child containers,
    /// false otherwise.
    /// </param>
    /// <typeparam name="T">
    /// The type of the <paramref name="component"/>.
    /// </typeparam>
    /// <returns>
    /// True if the <paramref name="component"/> was removed successfully,
    /// false otherwise.
    /// </returns>
    bool Remove<T>(T component, bool inherit) where T : IComponent;
    
    /// <summary>
    /// Removes all <see cref="IComponent"/>s
    /// of the specified <typeparamref name="T"/> from the container.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the <see cref="IComponent"/>s.
    /// </typeparam>
    void Clear<T>() where T : IComponent;
    
    /// <summary>
    /// Removes all <see cref="IComponent"/>s
    /// of the specified <typeparamref name="T"/> from the container.
    /// </summary>
    /// <param name="inherit">
    /// True if the <see cref="IComponent"/>s
    /// should be clear from all child containers,
    /// false otherwise.
    /// </param>
    /// <typeparam name="T">
    /// The type of the <see cref="IComponent"/>s.
    /// </typeparam>
    void Clear<T>(bool inherit) where T : IComponent;
}