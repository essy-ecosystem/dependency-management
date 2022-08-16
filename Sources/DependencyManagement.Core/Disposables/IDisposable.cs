namespace DependencyManagement.Core.Disposables;

/// <summary>
///     An <see cref="IDisposable" /> object which, when disposed, can disposed a <paramref name="Value" /> object.
/// </summary>
/// <typeparam name="T">The type of the <paramref name="Value" /> object.</typeparam>
/// <remarks>
///     Use <see cref="IDisposable{T}" />, if your object does not implement <see cref="IDisposable" />,
///     but you need to implement it externally.
/// </remarks>
/// <seealso cref="IDisposable" />
/// <seealso cref="IAsyncDisposable" />
/// <seealso cref="IAsyncDisposable{T}" />
public interface IDisposable<out T> : IDisposable
{
    /// <summary>
    ///     The object <typeparamref name="T" /> to dispose.
    /// </summary>
    T Value { get; }
}