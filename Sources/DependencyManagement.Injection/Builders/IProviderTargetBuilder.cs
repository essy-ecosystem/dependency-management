using DependencyManagement.Injection.Providers;

namespace DependencyManagement.Injection.Builders;

public interface IProviderTargetBuilder<T> : IStrategyTargetBuilder<T> where T : class
{
    IStrategyTargetBuilder<T> With(Func<IProvider<T>> provider);
}