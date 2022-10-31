namespace DependencyManagement.Providers;

using Containers;

public class InstanceProvider<T> : Provider<T> where T : notnull
{
    private readonly T _cache;

    public InstanceProvider(T cache)
    {
        _cache = cache;
    }

    public override T CreateInstance(IReadOnlyContainer container)
    {
        return _cache;
    }
}