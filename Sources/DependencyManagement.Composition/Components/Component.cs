using DependencyManagement.Composition.Composites;
using DependencyManagement.Core.Disposables;

namespace DependencyManagement.Composition.Components;

/// <summary>
///     This is a object that is stored in the <see cref="IComposite" />.
///     Also, this object is disposable (asynchronously too).
///     His lifetime is managed by the <see cref="IComposite" />.
/// </summary>
public abstract class Component : AsyncDisposableObject, IComponent
{
}