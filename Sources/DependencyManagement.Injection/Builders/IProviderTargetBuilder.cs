namespace DependencyManagement.Builders;

using Providers;

public interface IProviderTargetBuilder<T> : IStrategyTargetBuilder<T> where T : notnull
{
    IStrategyTargetBuilder<T> With<TProvider>(TProvider provider) where TProvider : IProvider<T>;
}