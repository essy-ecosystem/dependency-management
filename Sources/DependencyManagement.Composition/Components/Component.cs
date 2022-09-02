namespace DependencyManagement.Composition.Components;

using Composites;
using Core.Disposables;

/// <summary>
/// This is a object that is stored in the <see cref="IComposite" />.
/// Also, this object is disposable (asynchronously too).
/// His lifetime is managed by the <see cref="IComposite" />.
/// </summary>
public abstract class Component : AsyncDisposableObject, IComponent { }