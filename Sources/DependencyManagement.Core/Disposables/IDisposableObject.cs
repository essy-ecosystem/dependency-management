namespace DependencyManagement.Core.Disposables;

/// <summary>
///     An object with disposed status.
/// </summary>
public interface IDisposableObject : IDisposable
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