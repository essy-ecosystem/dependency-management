namespace DependencyManagement.Core.Initializables;

/// <summary>
///     An <see cref="IInitializable" /> object which, after creation, can initialize a <paramref name="Value" /> object.
/// </summary>
/// <typeparam name="T">The type of the <paramref name="Value" /> object.</typeparam>
/// <remarks>
///     Use <see cref="IInitializable{T}" />, if your object does not implement <see cref="IInitializable" />,
///     but you need to implement it externally.
/// </remarks>
/// <seealso cref="IInitializable" />
/// <seealso cref="IAsyncInitializable" />
/// <seealso cref="IAsyncInitializable{T}" />
public interface IInitializable<out T> : IInitializable
{
    /// <summary>
    ///     An object for initialization.
    /// </summary>
    T Value { get; }
}

/// <summary>
///     An <see cref="IInitializable" /> object that can be initialized after creation.
/// </summary>
/// <seealso cref="IInitializable{T}" />
/// <seealso cref="IAsyncInitializable" />
/// <seealso cref="IAsyncInitializable{T}" />
public interface IInitializable
{
    /// <summary>
    ///     Initializes the object.
    /// </summary>
    void Initialize();
}