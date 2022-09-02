namespace DependencyManagement.Composition.Containers;

using DependencyManagement.Composition.Components;
using DependencyManagement.Core.Disposables;

public interface IReadOnlyContainer : IDisposable, IAsyncDisposableObject
{
    IReadOnlyContainer? Father { get; }

    IReadOnlyList<T> All<T>() where T : class, IComponent;

    bool Contains<T>(T component) where T : class, IComponent;

    bool Any<T>() where T : class, IComponent;
}