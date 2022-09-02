namespace DependencyManagement.Core.Caches;

using System.Collections;
using Disposables;
using Utils;

/// <inheritdoc cref="DependencyManagement.Core.Caches.IDisposableCollection" />
public class DisposableCollection : AsyncDisposableObject, IDisposableCollection
{
    private readonly HashSet<object> _disposables = new();

    /// <inheritdoc />
    public int Count => _disposables.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public bool Add(object item)
    {
        ThrowUtils.ThrowIfDisposed(IsDisposed);

        return _disposables.Add(item);
    }

    void ICollection<object>.Add(object item)
    {
        Add(item);
    }

    /// <inheritdoc />
    public void Clear()
    {
        _disposables.Clear();
    }

    /// <inheritdoc />
    public bool Contains(object item)
    {
        return _disposables.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(object[] array, int arrayIndex)
    {
        _disposables.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(object item)
    {
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
        {
            foreach (var item in _disposables)
            {
                switch (item)
                {
                    case IDisposable disposable:
                        disposable.Dispose();
                        break;
                    case IAsyncDisposable asyncDisposable:
                        asyncDisposable.DisposeAsync().AsTask().Wait();
                        break;
                }
            }
        }

        _disposables.Clear();

        base.DisposeCore(disposing);
    }

    /// <inheritdoc />
    protected override async ValueTask DisposeCoreAsync()
    {
        foreach (var item in _disposables)
        {
            switch (item)
            {
                case IAsyncDisposable asyncDisposable:
                    await asyncDisposable.DisposeAsync();
                    break;
                case IDisposable disposable:
                    disposable.Dispose();
                    break;
            }
        }

        await base.DisposeCoreAsync();
    }
}