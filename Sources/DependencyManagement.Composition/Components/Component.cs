namespace DependencyManagement.Components;

using Disposables;

/// <inheritdoc cref="DependencyManagement.Components.IComponent"/>
public abstract class Component : AsyncDisposableObject, IComponent { }