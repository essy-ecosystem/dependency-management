namespace DependencyManagement.Disposables;

using Delegates;

/// <inheritdoc cref="DependencyManagement.Disposables.AsyncDisposable" />
public class AsyncDisposableObject<T> : AsyncDisposable<T>, IDisposableObject where T : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncDisposableObject{T}"/> class.
    /// </summary>
    /// <param name="value">
    /// The value to dispose.
    /// </param>
    /// <param name="dispose">
    /// The action to perform when disposing the value.
    /// </param>
    /// <param name="asyncDispose">
    /// The action to perform when disposing the value asynchronously.
    /// </param>
    public AsyncDisposableObject(T value, Func<T, ValueTask> asyncDispose, Action<T, bool>? dispose = null)
        : base(value, asyncDispose, dispose) { }

    /// <inheritdoc />
    public event DisposingDelegate? Disposing;

    /// <inheritdoc />
    public new bool IsDisposed => base.IsDisposed;

    /// <inheritdoc />
    protected override void DisposeCore(bool disposing)
    {
        Disposing?.Invoke(this);
        base.DisposeCore(disposing);
    }
}

/// <inheritdoc cref="DependencyManagement.Disposables.AsyncDisposable" />
public abstract class AsyncDisposableObject : AsyncDisposable, IDisposableObject
{
    /// <inheritdoc />
    public event DisposingDelegate? Disposing;

    /// <inheritdoc />
    public new bool IsDisposed => base.IsDisposed;

    /// <inheritdoc />
    protected override void DisposeCore(bool disposing)
    {
        Disposing?.Invoke(this);
        base.DisposeCore(disposing);
    }
}