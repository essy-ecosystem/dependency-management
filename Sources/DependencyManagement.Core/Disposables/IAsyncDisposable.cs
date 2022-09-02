namespace DependencyManagement.Core.Disposables;

/// <summary>
/// An <see cref="System.IDisposable" /> object which, when disposed, can disposed a Value object asynchronously.
/// </summary>
/// <typeparam name="T">The type of the Value object.</typeparam>
/// <remarks>
/// Use <see cref="DependencyManagement.Core.Disposables.IDisposable{T}" />,
/// if your object does not implement <see cref="System.IDisposable" />,
/// but you need to implement it externally.
/// </remarks>
/// <seealso cref="System.IDisposable" />
/// <seealso cref="System.IAsyncDisposable" />
/// <seealso cref="DependencyManagement.Core.Disposables.IDisposable{T}" />
public interface IAsyncDisposable<out T> : IAsyncDisposable
{
    /// <summary>
    /// The object <typeparamref name="T" /> to dispose.
    /// </summary>
    T Value { get; }
}