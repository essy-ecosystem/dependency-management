namespace DependencyManagement.Injection.Builders;

using Providers;

public interface IProviderTargetBuilder<T> : IStrategyTargetBuilder<T> where T : class
{
    IStrategyTargetBuilder<T> With(Func<IProvider<T>> provider);
}