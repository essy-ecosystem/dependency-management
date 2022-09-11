namespace DependencyManagement.Injection.Builders;

using Strategies;

public interface IStrategyTargetBuilder<T> : IElementaryTargetBuilder<T> where T : class
{
    void To<TStrategy>() where TStrategy : class, IStrategy;
}