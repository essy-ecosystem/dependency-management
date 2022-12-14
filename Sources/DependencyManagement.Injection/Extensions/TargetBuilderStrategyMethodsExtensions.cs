namespace DependencyManagement.Injection.Extensions;

using Builders;
using Composition.Containers;
using Providers;
using Strategies;

public static class TargetBuilderStrategyMethodsExtensions
{
    public static IAbstractionTargetBuilder<T> AsSelf<T>(this IAbstractionTargetBuilder<T> builder) where T : class
    {
        return builder.As<T>();
    }

    public static IStrategyTargetBuilder<T> With<T>(this IProviderTargetBuilder<T> builder, IProvider<T> provider)
        where T : class
    {
        return builder.With(() => provider);
    }

    public static IStrategyTargetBuilder<T> With<T>(this IProviderTargetBuilder<T> builder, T instance) where T : class
    {
        return builder.With(() => new InstanceProvider<T>(instance));
    }

    public static IStrategyTargetBuilder<T> With<T>(this IProviderTargetBuilder<T> builder,
        Func<IReadOnlyContainer, T> instance) where T : class
    {
        return builder.With(() => new LambdaProvider<T>(instance));
    }

    public static void ToSingleton<T>(this IStrategyTargetBuilder<T> builder) where T : class
    {
        builder.To<SingletonStrategy>();
    }

    public static void ToTransient<T>(this IStrategyTargetBuilder<T> builder) where T : class
    {
        builder.To<TransientStrategy>();
    }

    public static void ToScope<T>(this IStrategyTargetBuilder<T> builder) where T : class
    {
        builder.To<ScopeStrategy>();
    }

    public static void ToIsolatedSingleton<T>(this IStrategyTargetBuilder<T> builder) where T : class
    {
        builder.To<IsolatedSingletonStrategy>();
    }

    public static void ToIsolatedScope<T>(this IStrategyTargetBuilder<T> builder) where T : class
    {
        builder.To<IsolatedScopeStrategy>();
    }
}