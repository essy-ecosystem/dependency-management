namespace DependencyManagement.Injection.Providers;

using Composition.Containers;

public abstract class MethodProvider<T> : Provider<T> where T : notnull
{
    private readonly Func<IReadOnlyContainer, T> _cache;

    protected MethodProvider()
    {
        _cache = GetInstanceCore;
    }

    public override T GetInstance(IReadOnlyContainer container)
    {
        return _cache(container);
    }

    protected abstract T GetInstanceCore(IReadOnlyContainer container);
}