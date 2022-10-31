namespace DependencyManagement.Targets;

using Containers;
using Providers;

public sealed class UnsafeTransientTarget<T> : Target<T> where T : notnull
{
    private readonly IProvider<T> _provider;
    
    public UnsafeTransientTarget(IProvider<T> provider)
    {
        _provider = provider;
    }

    public override T GetInstance(IReadOnlyContainer container)
    {
        return _provider.CreateInstance(container);
    }

    public override bool ContainsInstance(T instance)
    {
        return false;
    }

    public override bool RemoveInstance(T instance)
    {
        return false;
    }
}