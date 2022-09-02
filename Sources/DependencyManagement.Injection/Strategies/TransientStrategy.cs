namespace DependencyManagement.Injection.Strategies;

using Composition.Containers;
using Core.Utils;
using Providers;

public sealed class TransientStrategy : Strategy
{
    public override T GetInstance<T>(IReadOnlyContainer container, IProvider<T> provider)
    {
        ThrowUtils.ThrowIfNull(container);
        ThrowUtils.ThrowIfNull(provider);
        ThrowUtils.ThrowIfDisposed(IsDisposed);

        var instance = provider.GetInstance(container);
        if (instance is null) throw new NullReferenceException(nameof(instance));

        return instance;
    }
}