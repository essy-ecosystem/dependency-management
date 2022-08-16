using DependencyManagement.Injection.Strategies;

namespace DependencyManagement.Injection.Builders;

public interface IStrategyTargetBuilder<T> : IElementaryTargetBuilder<T> where T : class
{
    void To<TStrategy>() where TStrategy : class, IStrategy;
}