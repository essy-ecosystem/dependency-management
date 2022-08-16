namespace DependencyManagement.Core.Disposables;

/// <inheritdoc cref="IDisposableObject" />
public interface IAsyncDisposableObject : IAsyncDisposable
{
    /// <summary>
    ///     Gets a value indicating whether this instance is disposed.
    /// </summary>
    /// <value>
    ///     True if this instance is disposed, false otherwise.
    /// </value>
    bool IsDisposed { get; }

    event EventHandler<bool> OnDisposing;
}