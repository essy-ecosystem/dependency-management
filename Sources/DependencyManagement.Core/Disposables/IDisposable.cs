namespace DependencyManagement.Disposables;

/// <summary>
/// An <see cref="System.IDisposable" /> object which, when disposed, can disposed a Value object.
/// </summary>
/// <typeparam name="T">
/// The type of the Value object.
/// </typeparam>
/// <remarks>
/// Use <see cref="IDisposable{T}" />,
/// if your object does not implement <see cref="System.IDisposable" />,
/// but you need to implement it externally.
/// </remarks>
/// <seealso cref="System.IDisposable" />
/// <seealso cref="System.IAsyncDisposable" />
/// <seealso cref="IAsyncDisposable{T}" />
public interface IDisposable<out T> : IDisposable
{
    /// <summary>
    /// The object with type <typeparamref name="T" /> to dispose.
    /// </summary>
    T Value { get; }
}