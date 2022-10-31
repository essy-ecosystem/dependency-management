namespace DependencyManagement.Initializables;

/// <inheritdoc cref="DependencyManagement.Initializables.IAsyncInitializable{T}" />
/// <seealso cref="DependencyManagement.Initializables.IAsyncInitializable{T}" />
public class AsyncInitializable<T> : IInitializable<T>, IAsyncInitializable<T> where T : notnull
{
    private readonly Func<T, ValueTask> _asyncInitialize;

    private readonly Action<T> _initialize;

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncInitializable{T}"/> class.
    /// </summary>
    /// <param name="value">
    /// The value object.
    /// </param>
    /// <param name="asyncInitialize">
    /// The asynchronous initializes delegate.
    /// </param>
    /// <param name="initialize">
    /// The initializes delegate.
    /// </param>
    public AsyncInitializable(T value, Func<T, ValueTask> asyncInitialize, Action<T>? initialize = null)
    {
        _initialize = initialize ?? DefaultInitialize;
        _asyncInitialize = asyncInitialize;
        Value = value;
    }

    /// <inheritdoc />
    public ValueTask InitializeAsync()
    {
        return _asyncInitialize(Value);
    }

    /// <inheritdoc cref="DependencyManagement.Initializables.IAsyncInitializable{T}.Value" />
    public T Value { get; }

    /// <inheritdoc />
    public void Initialize()
    {
        _initialize(Value);
    }

    private void DefaultInitialize(T value)
    {
        InitializeAsync().AsTask().Wait();
    }
}