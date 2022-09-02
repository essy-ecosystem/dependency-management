namespace DependencyManagement.Core.Initializables;

/// <summary>
/// An <see cref="IAsyncInitializable" /> object which, after creation, can initialize a <paramref name="Value" />
/// object asynchronously.
/// </summary>
/// <typeparam name="T">The type of the <paramref name="Value" /> object.</typeparam>
/// <remarks>
/// Use <see cref="IAsyncInitializable{T}" />, if your object does not implement <see cref="IAsyncInitializable" />,
/// but you need to implement it externally.
/// </remarks>
/// <seealso cref="IInitializable" />
/// <seealso cref="IInitializable{T}" />
/// <seealso cref="IAsyncInitializable" />
public interface IAsyncInitializable<out T> : IAsyncInitializable
{
    /// <summary>
    /// An object for initialization.
    /// </summary>
    T Value { get; }
}

/// <summary>
/// An <see cref="IInitializable" /> object that can be initialized after creation asynchronously.
/// </summary>
/// <seealso cref="IInitializable" />
/// <seealso cref="IInitializable{T}" />
/// <seealso cref="IAsyncInitializable{T}" />
public interface IAsyncInitializable
{
    /// <summary>
    /// Initializes the object asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask InitializeAsync();
}