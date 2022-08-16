namespace DependencyManagement.Core.Disposables;

/// <summary>
///     An <see cref="IAsyncDisposable" /> object which, when disposed, can disposed a <paramref name="Value" />
///     object asynchronously.
/// </summary>
/// <typeparam name="T">The type of the <paramref name="Value" /> object.</typeparam>
/// <remarks>
///     Use <see cref="IAsyncDisposable{T}" />, if your object does not implement <see cref="IAsyncDisposable" />,
///     but you need to implement it externally.
/// </remarks>
/// <seealso cref="IDisposable" />
/// <seealso cref="IAsyncDisposable" />
/// <seealso cref="IDisposable{T}" />
public interface IAsyncDisposable<out T> : IAsyncDisposable
{
    /// <summary>
    ///     The object <typeparamref name="T" /> to dispose.
    /// </summary>
    T Value { get; }
}