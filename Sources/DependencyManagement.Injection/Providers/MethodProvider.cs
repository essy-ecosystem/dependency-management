using DependencyManagement.Composition.Composites;

namespace DependencyManagement.Injection.Providers;

public abstract class MethodProvider<T> : Provider<T> where T : notnull
{
    private readonly Func<IReadOnlyComposite, T> _cache;

    protected MethodProvider()
    {
        _cache = GetInstanceCore;
    }

    public override T GetInstance(IReadOnlyComposite composite)
    {
        return _cache(composite);
    }

    protected abstract T GetInstanceCore(IReadOnlyComposite composite);
}