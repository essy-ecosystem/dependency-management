namespace DependencyManagement.Core.Caches;

using System.Collections;
using Disposables;
using Utils;

/// <inheritdoc cref="DependencyManagement.Core.Caches.IDisposableCollection" />
public class DisposableCollection : AsyncDisposableObject, IDisposableCollection
{
    private readonly HashSet<object> _disposablesObjects = new();
    
    public DisposableCollection() { }
    
    /// <param name="items">Elements to add.</param>
    public DisposableCollection(params object[] items)
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    /// <inheritdoc />
    public int Count => _disposablesObjects.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public bool Add(object item)
    {
        Thrower.ThrowIfArgumentNull(item);
        Thrower.ThrowIfObjectDisposed(IsDisposed);
        
        switch (item)
        {
            case IDisposableObject disposableObject:
                disposableObject.Disposing += OnItemOnDisposing;
                _disposablesObjects.Add(disposableObject);
                break;
            case IDisposable disposable:
                _disposablesObjects.Add(disposable);
                break;
            case IAsyncDisposable asyncDisposable:
                _disposablesObjects.Add(asyncDisposable);
                break;
        }

        return false;
    }

    private void OnItemOnDisposing(object item)
    {
        if (item is not IDisposableObject disposableObject) return;
        
        disposableObject.Disposing -= OnItemOnDisposing;
        _disposablesObjects.Remove(disposableObject);
    }

    void ICollection<object>.Add(object item)
    {
        Add(item);
    }

    /// <inheritdoc />
    public void Clear()
    {
        _disposablesObjects.Clear();
    }

    /// <inheritdoc />
    public bool Contains(object item)
    {
        return _disposablesObjects.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(object[] array, int arrayIndex)
    {
        _disposablesObjects.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc />
    public bool Remove(object item)
    {
        if (item is IDisposableObject disposableObject)
        {
            disposableObject.Disposing -= OnItemOnDisposing;
        }

        return _disposablesObjects.Remove(item);
    }

    /// <inheritdoc />
    public IEnumerator<object> GetEnumerator()
    {
        return _disposablesObjects.GetEnumerator();
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
            foreach (var item in _disposablesObjects)
            {
                switch (item)
                {
                    case IDisposableObject and IDisposable disposableObject:
                        ((IDisposableObject) disposableObject).Disposing -= OnItemOnDisposing;
                        disposableObject.Dispose();
                        break;
                    case IDisposableObject and IAsyncDisposable asyncDisposableObject:
                        ((IDisposableObject) asyncDisposableObject).Disposing -= OnItemOnDisposing;
                        asyncDisposableObject.DisposeAsync().GetAwaiter().GetResult();
                        break;
                    case IDisposable disposable:
                        disposable.Dispose();
                        break;
                    case IAsyncDisposable asyncDisposable:
                        asyncDisposable.DisposeAsync().GetAwaiter().GetResult();
                        break;
                }
            }
        }

        _disposablesObjects.Clear();

        base.DisposeCore(disposing);
    }

    /// <inheritdoc />
    protected override async ValueTask DisposeCoreAsync()
    {
        foreach (var item in _disposablesObjects)
        {
            switch (item)
            {
                case IDisposableObject and IAsyncDisposable asyncDisposableObject:
                    ((IDisposableObject) asyncDisposableObject).Disposing -= OnItemOnDisposing;
                    await asyncDisposableObject.DisposeAsync();
                    break;
                case IDisposableObject and IDisposable disposableObject:
                    ((IDisposableObject) disposableObject).Disposing -= OnItemOnDisposing;
                    disposableObject.Dispose();
                    break;
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