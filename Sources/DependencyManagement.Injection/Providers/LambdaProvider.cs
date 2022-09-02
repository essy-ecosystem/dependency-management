namespace DependencyManagement.Injection.Providers;

using Composition.Composites;

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