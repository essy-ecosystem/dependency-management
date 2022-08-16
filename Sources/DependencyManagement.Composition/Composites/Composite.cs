using System.Collections;
using System.Collections.Concurrent;
using DependencyManagement.Composition.Components;
using DependencyManagement.Core.Caches;
using DependencyManagement.Core.Disposables;
using DependencyManagement.Core.Utils;

namespace DependencyManagement.Composition.Composites;

/// <summary>
///     <inheritdoc cref="IComposite" />
///     The <see cref="Composite" /> is thread-safe.
/// </summary>
/// <inheritdoc cref="IComposite" />
public class Composite : AsyncDisposableObject, IComposite
{
    private readonly DisposableCache _cache = new();
    private readonly ConcurrentDictionary<Type, IList> _components = new();

    public Composite()
    {
    }

    public Composite(IComposite father) : this()
    {
        Father = father;
    }

    public IComposite? Father { get; }

    IReadOnlyComposite? IReadOnlyComposite.Father => Father;

    public IReadOnlyList<T> All<T>() where T : class, IComponent
    {
        return _components.TryGetValue(typeof(T), out var components)
            ? (IReadOnlyList<T>)components
            : new List<T>();
    }

    public bool Contains<T>(T component) where T : class, IComponent
    {
        ThrowUtils.ThrowIfNull(component);

        return _components.TryGetValue(typeof(T), out var components)
               && components.Contains(component);
    }

    public bool Any<T>() where T : class, IComponent
    {
        return _components.ContainsKey(typeof(T));
    }

    public void Add<T>(T component) where T : class, IComponent
    {
        ThrowUtils.ThrowIfNull(component);
        if (IsDisposed) throw new ObjectDisposedException(nameof(Composite));

        var type = typeof(T);

        if (_components.TryGetValue(type, out var components)) components.Add(component);
        else if (!_components.TryAdd(type, new List<T> { component })) Add(component);

        _cache.TryAdd(component);
    }

    public bool Remove<T>(T component) where T : class, IComponent
    {
        ThrowUtils.ThrowIfNull(component);

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
        if (disposing) _cache.Dispose();

        base.DisposeCore(disposing);
    }

    protected override async ValueTask DisposeCoreAsync()
    {
        await _cache.DisposeAsync();

        await base.DisposeCoreAsync();
    }
}