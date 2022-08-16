using DependencyManagement.Composition.Composites;
using DependencyManagement.Core.Utils;
using DependencyManagement.Injection.Providers;

namespace DependencyManagement.Injection.Strategies;

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