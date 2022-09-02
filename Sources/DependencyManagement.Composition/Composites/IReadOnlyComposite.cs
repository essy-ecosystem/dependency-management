namespace DependencyManagement.Composition.Composites;

using Components;
using Core.Disposables;

/// <summary>
/// This is a IoC container that can be used to resolve <see cref="IComponent" />.
/// </summary>
/// <remarks>
/// When <see cref="IComponent" /> is added to <see cref="IComposite" />, composite is started controlled his lifetime.
/// When <see cref="IComponent" /> is removed from <see cref="IComposite" />, composite is too controlled his lifetime.
/// When <see cref="IComposite" /> is disposed, all components are disposed too,
/// if this <see cref="IComposite" /> father is not contains they.
/// </remarks>
public interface IReadOnlyComposite : IDisposable, IAsyncDisposableObject
{
    IReadOnlyComposite? Father { get; }

    IReadOnlyList<T> All<T>() where T : class, IComponent;

    bool Contains<T>(T component) where T : class, IComponent;

    bool Any<T>() where T : class, IComponent;
}