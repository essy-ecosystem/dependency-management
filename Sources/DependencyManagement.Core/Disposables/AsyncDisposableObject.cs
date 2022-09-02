namespace DependencyManagement.Core.Disposables;

/// <inheritdoc cref="DependencyManagement.Core.Disposables.AsyncDisposable" />
public class AsyncDisposableObject<T> : AsyncDisposable<T>, IAsyncDisposableObject where T : notnull
{
    /// <param name="value">The value to dispose.</param>
    /// <param name="dispose">The action to perform when disposing the value.</param>
    /// <param name="asyncDispose">The action to perform when disposing the value asynchronously.</param>
    public AsyncDisposableObject(T value, Func<T, ValueTask> asyncDispose, Action<T, bool>? dispose = null)
        : base(value, asyncDispose, dispose) { }

    /// <inheritdoc />
    public event Action<object>? OnDisposing;

    /// <inheritdoc />
    public new bool IsDisposed => base.IsDisposed;

    /// <inheritdoc />
    protected override void DisposeCore(bool disposing)
    {
        OnDisposing?.Invoke(this);
        base.DisposeCore(disposing);
    }
}

/// <inheritdoc cref="DependencyManagement.Core.Disposables.AsyncDisposable" />
public abstract class AsyncDisposableObject : AsyncDisposable, IAsyncDisposableObject
{
    /// <inheritdoc />
    public event Action<object>? OnDisposing;

    /// <inheritdoc />
    public new bool IsDisposed => base.IsDisposed;

    /// <inheritdoc />
    protected override void DisposeCore(bool disposing)
    {
        OnDisposing?.Invoke(this);
        base.DisposeCore(disposing);
    }
}