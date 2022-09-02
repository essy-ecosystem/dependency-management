namespace DependencyManagement.Injection.Strategies;

using Composition.Composites;
using Core.Utils;
using Providers;

public sealed class TransientStrategy : Strategy
{
    public override T GetInstance<T>(IReadOnlyComposite composite, IProvider<T> provider)
    {
        ThrowUtils.ThrowIfNull(composite);
        ThrowUtils.ThrowIfNull(provider);
        ThrowUtils.ThrowIfDisposed(IsDisposed);

        var instance = provider.GetInstance(composite);
        if (instance is null) throw new NullReferenceException(nameof(instance));

        return instance;
    }
}