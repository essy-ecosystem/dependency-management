namespace DependencyManagement.Injection.Providers;

using Composition.Containers;

public abstract class MethodProvider<T> : Provider<T> where T : notnull
{
    private readonly Func<IReadOnlyContainer, T> _cache;

    protected MethodProvider()
    {
        _cache = CreateInstanceCore;
    }

    public override T CreateInstance(IReadOnlyContainer container)
    {
        return _cache(container);
    }

    protected abstract T CreateInstanceCore(IReadOnlyContainer container);
}