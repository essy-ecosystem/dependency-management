namespace DependencyManagement.Composition.Components;

public class LazyComponent<T> : Component, ILazyComponent<T> where T : class, IComponent
{
    private readonly Lazy<T> _lazy;

    public LazyComponent(Func<T> factory)
    {
        _lazy = new Lazy<T>(factory, true);
    }

    public T Value => _lazy.Value;

    public bool IsValueCreated => _lazy.IsValueCreated;

    protected override void DisposeCore(bool disposing)
    {
        if (disposing && IsValueCreated) Value.Dispose();

        base.DisposeCore(disposing);
    }

    protected override async ValueTask DisposeCoreAsync()
    {
        if (IsValueCreated) await Value.DisposeAsync();

        await base.DisposeCoreAsync();
    }
}