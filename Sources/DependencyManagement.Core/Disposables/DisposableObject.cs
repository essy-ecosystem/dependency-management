namespace DependencyManagement.Core.Disposables;

/// <inheritdoc cref="Disposable" />
public class DisposableObject<T> : Disposable<T>, IDisposableObject where T : notnull
{
    /// <param name="value">The value to dispose.</param>
    /// <param name="dispose">The action to perform when disposing the value.</param>
    public DisposableObject(T value, Action<T, bool> dispose) : base(value, dispose)
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

/// <inheritdoc cref="Disposable" />
public abstract class DisposableObject : Disposable, IDisposableObject
{
    public event EventHandler<bool>? OnDisposing;

    public new bool IsDisposed => base.IsDisposed;

    protected override void DisposeCore(bool disposing)
    {
        OnDisposing?.Invoke(this, disposing);
        base.DisposeCore(disposing);
    }
}