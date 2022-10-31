namespace DependencyManagement.Containers;

using Components;
using DependencyManagement.Disposables;

/// <summary>
/// Represents a read only container that can be used
/// to resolve <see cref="IComponent"/>.
/// </summary>
/// <seealso cref="IContainer"/>
public interface IReadOnlyContainer : IDisposable, IAsyncDisposable, IDisposableObject
{
    /// <summary>
    /// Father of this container.
    /// If container is root, this property is null.
    /// </summary>
    IReadOnlyContainer? Father { get; }
    
    /// <summary>
    /// Gets the <see cref="IComponent"/> read only list
    /// with the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the components.
    /// </typeparam>
    /// <returns>
    /// The <see cref="IComponent"/> read only list.
    /// </returns>
    IReadOnlyList<T> All<T>() where T : IComponent;
    
    /// <summary>
    /// Gets the <see cref="IComponent"/> read only list
    /// with the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the components.
    /// </typeparam>
    /// <param name="inherit">
    /// If true, the components of the fathers container will be included.
    /// </param>
    /// <returns>
    /// The <see cref="IComponent"/> read only list.
    /// </returns>
    IReadOnlyList<T> All<T>(bool inherit) where T : IComponent;
    
    /// <summary>
    /// Check if the container contains a <paramref name="component"/>
    /// with the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="component">
    /// The <see cref="IComponent"/> to check.
    /// </param>
    /// <typeparam name="T">
    /// Type of the <paramref name="component"/>.
    /// </typeparam>
    /// <returns>
    /// True if the container contains the <paramref name="component"/>
    /// with the specified type <typeparamref name="T"/>,
    /// false otherwise.
    /// </returns>
    bool Contains<T>(T component) where T : IComponent;
    
    /// <summary>
    /// Check if the container contains a <paramref name="component"/>
    /// with the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="component">
    /// The <see cref="IComponent"/> to check.
    /// </param>
    /// <typeparam name="T">
    /// Type of the <paramref name="component"/>.
    /// </typeparam>
    /// <param name="inherit">
    /// If true, check contains in the fathers container.
    /// </param>
    /// <returns>
    /// True if the container contains the <paramref name="component"/>
    /// with the specified type <typeparamref name="T"/>,
    /// false otherwise.
    /// </returns>
    bool Contains<T>(T component, bool inherit) where T : IComponent;
    
    /// <summary>
    /// Check if the container contains any <see cref="IComponent"/>
    /// with the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the components.
    /// </typeparam>
    /// <returns>
    /// True if the container contains any <see cref="IComponent"/>
    /// with the specified type <typeparamref name="T"/>,
    /// false otherwise.
    /// </returns>
    bool Any<T>() where T : IComponent;
    
    /// <summary>
    /// Check if the container contains any <see cref="IComponent"/>
    /// with the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the components.
    /// </typeparam>
    /// <param name="inherit">
    /// If true, check any in the fathers container.
    /// </param>
    /// <returns>
    /// True if the container contains any <see cref="IComponent"/>
    /// with the specified type <typeparamref name="T"/>,
    /// false otherwise.
    /// </returns>
    bool Any<T>(bool inherit) where T : IComponent;
}