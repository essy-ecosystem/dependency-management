using DependencyManagement.Composition.Composites;

namespace DependencyManagement.Injection.Providers;

public class LambdaProvider<T> : Provider<T> where T : notnull
{
    private readonly Func<IReadOnlyComposite, T> _cache;

    public LambdaProvider(Func<IReadOnlyComposite, T> provider)
    {
        _cache = provider;
    }

    public override T GetInstance(IReadOnlyComposite composite)
    {
        return _cache(composite);
    }
}