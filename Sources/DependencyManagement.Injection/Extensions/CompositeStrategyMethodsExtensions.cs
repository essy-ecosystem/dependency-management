namespace DependencyManagement.Injection.Extensions;

using Composition.Components;
using Composition.Composites;
using Composition.Extensions;
using Composition.Utils;
using Strategies;

public static class CompositeStrategyMethodsExtensions
{
    public static bool TrySetLazyStrategy<T>(this IComposite composite, Func<T> strategy) where T : class, IStrategy
    {
        var rootComposite = CompositeTreeUtils.GetLast(composite);
        var lazy = new LazyComponent<T>(strategy);
        if (!rootComposite.TrySetLazy(lazy)) return false;
        if (rootComposite.TryAddLazy<IStrategy>(lazy)) return true;
        rootComposite.RemoveLazy<IStrategy>(lazy);
        return false;
    }

    public static T WithStrategies<T>(this T composite) where T : class, IComposite
    {
        var rootComposite = CompositeTreeUtils.GetLast(composite);

        rootComposite.TrySetLazyStrategy(() => new TransientStrategy());
        rootComposite.TrySetLazyStrategy(() => new SingletonStrategy());
        rootComposite.TrySetLazyStrategy(() => new ThreadStrategy());
        rootComposite.TrySetLazyStrategy(() => new CompositeStrategy());
        rootComposite.TrySetLazyStrategy(() => new ThreadCompositeStrategy());

        return composite;
    }
}