namespace DependencyManagement.Composition.Containers;

using System.Collections;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Components;
using Core.Caches;
using Core.Disposables;
using Core.Utils;

public class Container : AsyncDisposableObject, IContainer
{
    private readonly ConcurrentDictionary<Type, IList> _components = new();
    
    private readonly DisposableCollection _disposableCollection = new();

    public Container() { }

    public Container(IContainer father) : this()
    {
        Father = father;
    }

    public IContainer? Father { get; }

    IReadOnlyContainer? IReadOnlyContainer.Father => Father;

    public IReadOnlyList<T> All<T>() where T : class, IComponent
    {
        return _components.TryGetValue(typeof(T), out var components)
            ? Unsafe.As<IReadOnlyList<T>>(components)
            : Array.Empty<T>();
    }

    public bool Contains<T>(T component) where T : class, IComponent
    {
        Thrower.ThrowIfArgumentNull(component);

        return _components.TryGetValue(typeof(T), out var components)
            && components.Contains(component);
    }

    public bool Any<T>() where T : class, IComponent
    {
        return _components.ContainsKey(typeof(T));
    }

    public void Add<T>(T component) where T : class, IComponent
    {
        Thrower.ThrowIfArgumentNull(component);

        if (IsDisposed) throw new ObjectDisposedException(nameof(Container));

        var type = typeof(T);

        if (_components.TryGetValue(type, out var components))
        {
            components.Add(component);
        }
        else if (!_components.TryAdd(type, new List<T> { component })) Add(component);

        _disposableCollection.Add(component);
    }

    public bool Remove<T>(T component) where T : class, IComponent
    {
        Thrower.ThrowIfArgumentNull(component);

        if (!_components.TryGetValue(typeof(T), out var components)) return false;

        var index = components.IndexOf(component);
        if (index < 0) return false;

        components.RemoveAt(index);

        return true;
    }

    public void Clear<T>() where T : class, IComponent
    {
        _components.TryRemove(typeof(T), out _);
    }

    protected override void DisposeCore(bool disposing)
    {
        if (disposing) _disposableCollection.Dispose();

        base.DisposeCore(disposing);
    }

    protected override async ValueTask DisposeCoreAsync()
    {
        await _disposableCollection.DisposeAsync();

        await base.DisposeCoreAsync();
    }
}