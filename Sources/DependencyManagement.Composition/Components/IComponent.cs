namespace DependencyManagement.Composition.Components;

using Containers;
using Core.Disposables;

/// <summary>
/// This is a object that is stored in the <see cref="IContainer" />.
/// Also, this object is disposable (asynchronously too).
/// His lifetime is managed by the <see cref="IContainer" />.
/// </summary>
public interface IComponent : IDisposable, IAsyncDisposable, IDisposableObject { }