namespace DependencyManagement.Components;

using DependencyManagement.Disposables;

/// <summary>
/// A component that can be used to compose a dependency management system.
/// </summary>
public interface IComponent : IDisposable, IAsyncDisposable, IDisposableObject { }