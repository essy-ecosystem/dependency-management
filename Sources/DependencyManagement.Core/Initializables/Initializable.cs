namespace DependencyManagement.Core.Initializables;

/// <inheritdoc cref="IInitializable{T}" />
/// <seealso cref="IInitializable{T}" />
public class Initializable<T> : IInitializable<T> where T : notnull
{
    private readonly Action<T> _initialize;

    /// <param name="value">The value object.</param>
    /// <param name="initialize">The initializes delegate.</param>
    public Initializable(T value, Action<T> initialize)
    {
        Value = value;
        _initialize = initialize;
    }

    public T Value { get; }

    public void Initialize()
    {
        _initialize(Value);
    }
}