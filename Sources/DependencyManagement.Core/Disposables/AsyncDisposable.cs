namespace DependencyManagement.Core.Disposables;

/// <inheritdoc cref="IAsyncDisposable{T}" />
public class AsyncDisposable<T> : AsyncDisposable, IDisposable<T>, IAsyncDisposable<T> where T : notnull
{
    private readonly Func<T, ValueTask> _asyncDispose;
    private readonly Action<T, bool>? _dispose;

    /// <param name="value">The value to dispose.</param>
    /// <param name="dispose">The action to perform when disposing the value.</param>
    /// <param name="asyncDispose">The action to perform when disposing the value asynchronously.</param>
    public AsyncDisposable(T value, Func<T, ValueTask> asyncDispose, Action<T, bool>? dispose = null)
    {
        Value = value;

        _dispose = dispose;
        _asyncDispose = asyncDispose;
    }

    public T Value { get; }

    protected override void DisposeCore(bool disposing)
    {
        if (_dispose is not null) _dispose(Value, disposing);
        else if (disposing) _asyncDispose(Value).AsTask().Wait();

        base.DisposeCore(disposing);
    }

    protected override async ValueTask DisposeCoreAsync()
    {
        await _asyncDispose(Value);

        await base.DisposeCoreAsync();
    }
}

/// <inheritdoc cref="IAsyncDisposable" />
public abstract class AsyncDisposable : IAsyncDisposable, IDisposable
{
    /// <summary>
    ///     Gets a value indicating whether this instance is disposed.
    /// </summary>
    /// <value>
    ///     True if this instance is disposed, and otherwise, false.
    /// </value>
    protected bool IsDisposed { get; private set; }

    public async ValueTask DisposeAsync()
    {
        if (IsDisposed) return;

        await DisposeCoreAsync();
        DisposeCore(false);

        GC.SuppressFinalize(this);
        IsDisposed = true;
    }

    public void Dispose()
    {
        if (IsDisposed) return;

        DisposeCore(true);

        GC.SuppressFinalize(this);
        IsDisposed = true;
    }

    /// <summary>
    ///     Releases unmanaged and (optionally) managed resources.
    /// </summary>
    /// <param name="disposing">
    ///     True to release both managed and unmanaged resources,
    ///     and False to release only unmanaged resources.
    /// </param>
    protected virtual void DisposeCore(bool disposing)
    {
    }

    /// <summary>
    ///     Releases managed resources asynchronously.
    /// </summary>
    /// <returns>A <see cref="ValueTask" /> that represents the asynchronous operation.</returns>
    protected virtual ValueTask DisposeCoreAsync()
    {
        return default;
    }

    ~AsyncDisposable()
    {
        DisposeCore(false);
    }
}