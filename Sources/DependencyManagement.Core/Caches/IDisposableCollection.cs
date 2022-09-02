namespace DependencyManagement.Core.Caches;

/// <summary>
/// A cache collection that stores <see cref="System.IDisposable" /> or <see cref="System.IAsyncDisposable" /> objects,
/// which, when the cache <see cref="DependencyManagement.Core.Caches.IDisposableCollection.Dispose()" />
/// or <see cref="DependencyManagement.Core.Caches.IDisposableCollection.DisposeAsync()" />
/// method invoked, they also disposed.
/// </summary>
/// <remarks>
/// Use <see cref="DependencyManagement.Core.Caches.IDisposableCollection" /> when you need to collect a large number of
/// not null, <see cref="System.IDisposable" /> or <see cref="System.IAsyncDisposable" /> objects,
/// and then dispose them safely and simultaneously, or enumerable them.
/// </remarks>
/// <example>
/// The usage example:
/// <code>
/// var collection = new DisposableCollection();
/// collection.Add(obj1);
/// collection.Add(obj2);
/// // ...
/// if (collection.Add(obj999) { /* Do something. */ }
/// if (IsSync) collection.Dispose();
/// else await collection.DisposeAsync();
/// </code>
/// </example>
/// <seealso cref="System.IDisposable" />
/// <seealso cref="System.IAsyncDisposable" />
public interface IDisposableCollection : ICollection<object>, IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Adds an <paramref name="item" /> to the collection if it
    /// is <see cref="System.IDisposable" />or <see cref="System.IAsyncDisposable" />
    /// and is not contained in the collection.
    /// </summary>
    /// <param name="item">The item that will be added.</param>
    /// <exception cref="System.ObjectDisposedException">Thrown if the container has already been disposed.</exception>
    new bool Add(object item);
}