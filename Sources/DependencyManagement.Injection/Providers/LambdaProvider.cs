namespace DependencyManagement.Injection.Providers;

using Composition.Containers;

public class LambdaProvider<T> : Provider<T> where T : notnull
{
    private readonly Func<IReadOnlyContainer, T> _cache;

    public LambdaProvider(Func<IReadOnlyContainer, T> provider)
    {
        _cache = provider;
    }

    public override T CreateInstance(IReadOnlyContainer container)
    {
        return _cache(container);
    }
}