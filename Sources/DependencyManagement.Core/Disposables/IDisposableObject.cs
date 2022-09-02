namespace DependencyManagement.Core.Disposables;

/// <summary>
/// <inheritdoc cref="System.IDisposable" />
/// Like <see cref="System.IDisposable" /> but with disposable status and events.
/// </summary>
/// <inheritdoc cref="System.IDisposable" />
public interface IDisposableObject : IDisposable
{
    /// <summary>
    /// Gets a value indicating whether this instance is disposed.
    /// </summary>
    /// <value>
    /// True if this instance is disposed, false otherwise.
    /// </value>
    bool IsDisposed { get; }

    /// <summary>
    /// This event is called before an object dispose to notify that the object
    /// has been disposing and to do something before it is disposed.
    /// </summary>
    /// <remarks>
    /// It should not be asynchronous.
    /// </remarks>
    event Action<object> OnDisposing;
}