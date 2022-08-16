namespace DependencyManagement.Core.Caches;

/// <summary>
///     A cache that stores not null, <see cref="IDisposable" /> or <see cref="IAsyncDisposable" /> objects,
///     which, when the cache <see cref="IDisposableCache.Dispose()" /> or <see cref="IDisposableCache.DisposeAsync()" />
///     method invoked, they also disposed.
/// </summary>
/// <remarks>
///     Use <see cref="IDisposableCache" /> when you need to collect a large number of
///     not null, <see cref="IDisposable" /> or <see cref="IAsyncDisposable" /> objects,
///     and then dispose them safely and simultaneously, or enumerable them.
/// </remarks>
public interface IDisposableCache : IEnumerable<object>, IDisposable, IAsyncDisposable
{
    /// <summary>
    ///     Trying to add an <paramref name="item" /> to the cache.
    ///     If the <paramref name="item" /> is not null,
    ///     <see cref="IDisposableCache.Dispose()" /> or <see cref="IDisposableCache.DisposeAsync()" /> object
    ///     it will be added to the cache, false otherwise.
    /// </summary>
    /// <param name="item">An object that can be added to the cache.</param>
    /// <returns>True if the object was added to the cache, false otherwise.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the cache has already been disposed yet.</exception>
    public bool TryAdd(object? item);

    /// <summary>
    ///     Trying to remove an <paramref name="item" /> to the cache.
    ///     If the <paramref name="item" /> is null,
    ///     or it will not be added to the cache, false otherwise.
    /// </summary>
    /// <param name="item">An object that can be removed from the cache.</param>
    /// <returns>True if the object was removed from the cache, false otherwise.</returns>
    public bool TryRemove(object? item);
}