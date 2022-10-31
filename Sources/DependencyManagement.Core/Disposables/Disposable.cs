namespace DependencyManagement.Disposables;

/// <inheritdoc cref="DependencyManagement.Disposables.IDisposable{T}" />
public class Disposable<T> : Disposable, IDisposable<T> where T : notnull
{
    private readonly Action<T, bool> _dispose;

    /// <summary>
    /// Initializes a new instance of the <see cref="Disposable{T}"/> class.
    /// </summary>
    /// <param name="value">
    /// The value to dispose.
    /// </param>
    /// <param name="dispose">
    /// The action to perform when disposing the value.
    /// </param>
    // ReSharper disable once MemberCanBeProtected.Global
    public Disposable(T value, Action<T, bool> dispose)
    {
        Value = value;

        _dispose = dispose;
    }

    /// <inheritdoc />
    public T Value { get; }

    /// <inheritdoc />
    protected override void DisposeCore(bool disposing)
    {
        _dispose(Value, disposing);

        base.DisposeCore(disposing);
    }
}

/// <inheritdoc cref="System.IDisposable" />
public abstract class Disposable : IDisposable
{
    /// <summary>
    /// Gets a value indicating whether this instance is disposed.
    /// </summary>
    /// <value>
    /// True if this instance is disposed, and otherwise, false.
    /// </value>
    protected bool IsDisposed { get; private set; }

    /// <inheritdoc />
    public void Dispose()
    {
        if (IsDisposed) return;

        DisposeCore(true);

        GC.SuppressFinalize(this);
        IsDisposed = true;
    }

    /// <summary>
    /// Releases unmanaged and (optionally) managed resources.
    /// </summary>
    /// <param name="disposing">
    /// True to release both managed and unmanaged resources,
    /// and False to release only unmanaged resources.
    /// </param>
    protected virtual void DisposeCore(bool disposing) { }

    ~Disposable()
    {
        DisposeCore(false);
    }
}