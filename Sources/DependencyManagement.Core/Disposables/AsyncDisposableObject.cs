namespace DependencyManagement.Core.Disposables;

/// <inheritdoc cref="AsyncDisposable" />
public class AsyncDisposableObject<T> : AsyncDisposable<T>, IAsyncDisposableObject where T : notnull
{
    /// <param name="value">The value to dispose.</param>
    /// <param name="dispose">The action to perform when disposing the value.</param>
    /// <param name="asyncDispose">The action to perform when disposing the value asynchronously.</param>
    public AsyncDisposableObject(T value, Func<T, ValueTask> asyncDispose, Action<T, bool>? dispose = null)
        : base(value, asyncDispose, dispose)
    {
    }

    public event EventHandler<bool>? OnDisposing;

    public new bool IsDisposed => base.IsDisposed;

    protected override void DisposeCore(bool disposing)
    {
        OnDisposing?.Invoke(this, disposing);
        base.DisposeCore(disposing);
    }
}

/// <inheritdoc cref="AsyncDisposable" />
public abstract class AsyncDisposableObject : AsyncDisposable, IAsyncDisposableObject
{
    public event EventHandler<bool>? OnDisposing;

    public new bool IsDisposed => base.IsDisposed;

    protected override void DisposeCore(bool disposing)
    {
        OnDisposing?.Invoke(this, disposing);
        base.DisposeCore(disposing);
    }
}