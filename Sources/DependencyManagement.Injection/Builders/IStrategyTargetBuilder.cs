namespace DependencyManagement.Builders;

using Strategies;

public interface IStrategyTargetBuilder<T> : IElementaryTargetBuilder<T> where T : notnull
{
    void To<TStrategy>(TStrategy strategy) where TStrategy : IStrategy;
}