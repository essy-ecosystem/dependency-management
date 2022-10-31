namespace DependencyManagement.Components;

/// <summary>
/// A lazy component that can be used to compose a dependency management system.
/// </summary>
/// <typeparam name="T">
/// The type of the created component.
/// </typeparam>
public interface ILazyComponent<out T> : IComponent where T : IComponent
{
    /// <summary>
    /// Gets the component.
    /// Component created once and then cached.
    /// </summary>
    T Value { get; }
    
    /// <summary>
    /// Checks if the component is created.
    /// </summary>
    bool IsCreated { get; }
}