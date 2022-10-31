namespace DependencyManagement.Containers;

using System.Collections;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Components;
using Disposables;

/// <inheritdoc cref="DependencyManagement.Containers.IContainer"/>
public class Container : AsyncDisposableObject, IContainer
{
    private ConcurrentDictionary<Type, IList> _components = new();
    
    private HashSet<IComponent> _disposables = new();
    
    /// <summary>
    /// Creates a new instance of <see cref="Container"/> class.
    /// </summary>
    public Container() { }
    
    /// <summary>
    /// Creates a new instance of <see cref="Container"/> class.
    /// </summary>
    /// <param name="father">
    /// The father container.
    /// </param>
    public Container(IContainer father)
    {
        Father = father;
    }

    /// <inheritdoc/>
    public IContainer? Father { get; }

    /// <inheritdoc/>
    IReadOnlyContainer? IReadOnlyContainer.Father => Father;
  
    /// <inheritdoc/>
    public void Add<T>(T component) where T : IComponent
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        
        var type = typeof(T);
        
        AddCore(type, component);
    }

    private void AddCore<T>(Type type, T component) where T : IComponent
    {
        if (_components.TryGetValue(type, out var list) is false)
        {
            list = new List<T>{component};

            if (_components.TryAdd(type, list) is false)
            {
                AddCore(type, component);
                return;
            }
        }
        else
        {
            list.Add(component);
        }
        
        _disposables.Add(component);
    }

    /// <inheritdoc/>
    public bool Remove<T>(T component) where T : IComponent
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        
        var type = typeof(T);
        
        if (_components.TryGetValue(type, out var list)is false ) return false;

        var castedList = Unsafe.As<IList<T>>(list);
        
        var result = castedList.Remove(component);
        
        if (castedList.Count == 0) _components.TryRemove(type, out _);

        return result;
    }

    /// <inheritdoc/>
    public bool Remove<T>(T component, bool inherit) where T : IComponent
    {
        if (inherit is false) Remove(component);
        
        var container = Unsafe.As<IContainer>(this);

        do
        {
            if (container.Remove(component)) return true;
            container = container.Father;
        } while (container is not null);

        return false;
    }

    /// <inheritdoc/>
    public void Clear<T>() where T : IComponent
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        
        var type = typeof(T);
        
        _components.TryRemove(type, out _);
    }

    /// <inheritdoc/>
    public void Clear<T>(bool inherit) where T : IComponent
    {
        if (inherit is false) Clear<T>();
        
        var container = Unsafe.As<IContainer>(this);

        do
        {
            container.Clear<T>();
            container = container.Father;
        } while (container is not null);
    }

    /// <inheritdoc/>
    public IReadOnlyList<T> All<T>() where T : IComponent
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        
        var type = typeof(T);

        return _components.TryGetValue(type, out var list) 
            ? Unsafe.As<IReadOnlyList<T>>(list)
            : Array.Empty<T>();
    }
    
    /// <inheritdoc/>
    public IReadOnlyList<T> All<T>(bool inherit) where T : IComponent
    {
        if (inherit is false || Father is null) return All<T>();
        
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        
        var container = Unsafe.As<IReadOnlyContainer>(this);
        var components = new List<T>(All<T>());

        while (container.Father is not null)
        {
            container = container.Father!;
            components.AddRange(container.All<T>());
        }
        
        return components;
    }

    /// <inheritdoc/>
    public bool Contains<T>(T component) where T : IComponent
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        
        var type = typeof(T);
        
        return _components.TryGetValue(type, out var list)
            && list.Contains(component);
    }

    /// <inheritdoc/>
    public bool Contains<T>(T component, bool inherit) where T : IComponent
    {
        if (inherit is false) Contains(component);
        
        var container = Unsafe.As<IContainer>(this);

        do
        {
            if (container.Contains(component)) return true;
            container = container.Father;
        } while (container is not null);

        return false;
    }

    /// <inheritdoc/>
    public bool Any<T>() where T : IComponent
    {
        ObjectDisposedException.ThrowIf(IsDisposed, this);
        
        var type = typeof(T);
        
        return _components.TryGetValue(type, out var list)
            && list.Count > 0;
    }

    /// <inheritdoc/>
    public bool Any<T>(bool inherit) where T : IComponent
    {
        if (inherit is false) Any<T>();
        
        var container = Unsafe.As<IContainer>(this);

        do
        {
            if (container.Any<T>()) return true;
            container = container.Father;
        } while (container is not null);

        return false;
    }

    /// <inheritdoc/>
    protected override void DisposeCore(bool disposing)
    {
        if (disposing)
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }

        _components = null!;
        _disposables = null!;
    }

    /// <inheritdoc/>
    protected override async ValueTask DisposeCoreAsync()
    {
        foreach (var disposable in _disposables)
        {
            await disposable.DisposeAsync();
        }
    }
}