namespace DependencyManagement.Injection.Extensions;

using Composition.Components;
using Composition.Containers;
using Composition.Extensions;
using Composition.Utils;
using Strategies;

public static class CompositeStrategyMethodsExtensions
{
    public static bool TrySetLazyStrategy<T>(this IContainer container, Func<T> strategy) where T : class, IStrategy
    {
        var rootComposite = ContainerTreeUtils.GetLast(container);
        var lazy = new LazyComponent<T>(strategy);
        if (!rootComposite.TrySetLazy(lazy)) return false;
        if (rootComposite.TryAddLazy<IStrategy>(lazy)) return true;
        rootComposite.RemoveLazy<IStrategy>(lazy);
        return false;
    }

    public static T WithStrategies<T>(this T composite) where T : class, IContainer
    {
        var rootComposite = ContainerTreeUtils.GetLast(composite);

        rootComposite.TrySetLazyStrategy(() => new TransientStrategy());
        rootComposite.TrySetLazyStrategy(() => new SingletonStrategy());
        rootComposite.TrySetLazyStrategy(() => new ThreadStrategy());
        rootComposite.TrySetLazyStrategy(() => new ContainerStrategy());
        rootComposite.TrySetLazyStrategy(() => new ThreadCompositeStrategy());

        return composite;
    }
}