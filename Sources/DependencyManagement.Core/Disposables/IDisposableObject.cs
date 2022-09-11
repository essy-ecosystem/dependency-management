namespace DependencyManagement.Core.Disposables;

using Delegates;

/// <summary>
/// Represents statues about the disposal of the object,
/// and notifies about the disposal, before it.
/// </summary>
/// <remarks>
/// Recommended for use with standard
/// <see cref="System.IDisposable" />,or <see cref="System.IAsyncDisposable" /> interfaces.
/// </remarks>
/// <seealso cref="System.IDisposable" />
/// <seealso cref="System.IAsyncDisposable" />
public interface IDisposableObject
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
    event DisposingDelegate Disposing;
}