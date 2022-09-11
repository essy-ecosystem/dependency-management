namespace DependencyManagement.Core.Disposables;

using Delegates;

/// <inheritdoc cref="DependencyManagement.Core.Disposables.Disposable" />
public class DisposableObject<T> : Disposable<T>, IDisposableObject where T : notnull
{
    /// <param name="value">The value to dispose.</param>
    /// <param name="dispose">The action to perform when disposing the value.</param>
    public DisposableObject(T value, Action<T, bool> dispose) : base(value, dispose) { }

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

/// <inheritdoc cref="DependencyManagement.Core.Disposables.Disposable" />
public abstract class DisposableObject : Disposable, IDisposableObject
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