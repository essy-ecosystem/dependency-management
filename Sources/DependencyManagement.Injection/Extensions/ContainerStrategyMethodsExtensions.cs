namespace DependencyManagement.Injection.Extensions;

using Composition.Components;
using Composition.Containers;
using Composition.Extensions;
using Composition.Utils;
using Strategies;

public static class ContainerStrategyMethodsExtensions
{
    public static bool TrySetLazyStrategy<T>(this IContainer container, Func<T> strategy) where T : class, IStrategy
    {
        var rootComposite = TraversalService.GetInitial(container);
        var lazy = new LazyComponent<T>(strategy);
        if (!rootComposite.TrySetLazy(lazy)) return false;
        if (rootComposite.TryAddLazy<IStrategy>(lazy)) return true;
        rootComposite.RemoveLazy<IStrategy>(lazy);
        return false;
    }

    public static T WithStrategies<T>(this T composite) where T : class, IContainer
    {
        var rootComposite = TraversalService.GetInitial(composite);

        rootComposite.TrySetLazyStrategy(() => new TransientStrategy());
        rootComposite.TrySetLazyStrategy(() => new SingletonStrategy());
        rootComposite.TrySetLazyStrategy(() => new IsolatedSingletonStrategy());
        rootComposite.TrySetLazyStrategy(() => new ScopeStrategy());
        rootComposite.TrySetLazyStrategy(() => new IsolatedScopeStrategy());

        return composite;
    }
}