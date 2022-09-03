namespace DependencyManagement.Injection.Extensions;

using Builders;
using Composition.Containers;
using Providers;
using Strategies;
using Targets;

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

    public static ITarget<T> ToSingleton<T>(this IStrategyTargetBuilder<T> builder) where T : class
    {
        return builder.To<SingletonStrategy>();
    }

    public static ITarget<T> ToTransient<T>(this IStrategyTargetBuilder<T> builder) where T : class
    {
        return builder.To<TransientStrategy>();
    }

    public static ITarget<T> ToContainer<T>(this IStrategyTargetBuilder<T> builder) where T : class
    {
        return builder.To<ContainerStrategy>();
    }

    public static ITarget<T> ToThread<T>(this IStrategyTargetBuilder<T> builder) where T : class
    {
        return builder.To<ThreadStrategy>();
    }

    public static ITarget<T> ToThreadContainer<T>(this IStrategyTargetBuilder<T> builder) where T : class
    {
        return builder.To<ThreadContainerStrategy>();
    }
}