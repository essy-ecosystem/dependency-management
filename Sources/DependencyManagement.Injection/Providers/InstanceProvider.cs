using DependencyManagement.Composition.Composites;

namespace DependencyManagement.Injection.Providers;

public class InstanceProvider<T> : Provider<T> where T : notnull
{
    private readonly T _cache;

    public InstanceProvider(T cache)
    {
        _cache = cache;
    }

    public override T GetInstance(IReadOnlyComposite composite)
    {
        return _cache;
    }
}