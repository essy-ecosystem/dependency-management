using System.Collections;
using DependencyManagement.Core.Disposables;
using DependencyManagement.Core.Utils;

namespace DependencyManagement.Core.Caches;

/// <inheritdoc cref="DependencyManagement.Core.Caches.IDisposableCache" />
public class DisposableCache : AsyncDisposableObject, IDisposableCache
{
    private readonly HashSet<object> _disposables = new();

    /// <inheritdoc />
    public bool TryAdd(object? item)
    {
        ThrowUtils.ThrowIfDisposed(IsDisposed);

        return item is IDisposable or IAsyncDisposable && _disposables.Add(item);
    }

    /// <inheritdoc />
    public bool TryRemove(object? item)
    {
        if (item is null) return false;
        return _disposables.Remove(item);
    }

    /// <inheritdoc />
    public IEnumerator<object> GetEnumerator()
    {
        return _disposables.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    protected override void DisposeCore(bool disposing)
    {
        if (disposing)
            foreach (var item in _disposables)
                if (item is IDisposable disposable) disposable.Dispose();
                else if (item is IAsyncDisposable asyncDisposable) asyncDisposable.DisposeAsync().AsTask().Wait();

        _disposables.Clear();

        base.DisposeCore(disposing);
    }

    /// <inheritdoc />
    protected override async ValueTask DisposeCoreAsync()
    {
        foreach (var item in _disposables)
            if (item is IAsyncDisposable asyncDisposable) await asyncDisposable.DisposeAsync();
            else if (item is IDisposable disposable) disposable.Dispose();

        await base.DisposeCoreAsync();
    }
}