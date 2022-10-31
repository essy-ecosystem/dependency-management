namespace DependencyManagement.Components;

using Disposables;

/// <inheritdoc cref="DependencyManagement.Components.ILazyComponent{T}"/>
public class LazyComponent<T> : AsyncDisposableObject, ILazyComponent<T> where T : IComponent {
    private readonly object _lock = new();
    
    private T? _instance;
    
    private Func<T>? _factory;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="LazyComponent{T}"/> class.
    /// </summary>
    /// <param name="factory">
    /// The factory that creates the component.
    /// </param>
    public LazyComponent(Func<T> factory) {
        _factory = factory;
    }
    
    /// <inheritdoc/>
    public T Value
    {
        get
        {
            if (_instance is null) TryCreateInstance();

            return _instance!;
        }
    }
    
    /// <inheritdoc/>
    public bool IsCreated => _instance is not null;
    
    private void TryCreateInstance()
    {
        lock (_lock)
        {
            if (_factory is null) return;

            _instance = _factory();
            _factory = null;
        }
    }
}