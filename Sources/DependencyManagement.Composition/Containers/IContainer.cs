namespace DependencyManagement.Composition.Containers;

using Components;

public interface IContainer : IReadOnlyContainer
{
    new IContainer? Father { get; }

    /// <summary>
    /// Add <see cref="IComponent" /> to <see cref="IContainer" />.
    /// </summary>
    /// <param name="component"><see cref="IComponent" /> to add.</param>
    /// <typeparam name="T">Type of <see cref="IComponent" />.</typeparam>
    void Add<T>(T component) where T : class, IComponent;

    /// <summary>
    /// Remove first <paramref name="component" /> from this <see cref="IContainer" />, or his father.
    /// </summary>
    /// <param name="component"><see cref="IComponent" /> to remove.</param>
    /// <typeparam name="T">Type of <paramref name="component" />.</typeparam>
    /// <returns>True if <paramref name="component" /> was contains, and removed, false otherwise.</returns>
    bool Remove<T>(T component) where T : class, IComponent;

    /// <summary>
    /// Clear all <see name="IComponent" /> from this <see cref="IContainer" />,
    /// or his father with <typeparamref name="T" /> type.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IComponent" />.</typeparam>
    void Clear<T>() where T : class, IComponent;
}