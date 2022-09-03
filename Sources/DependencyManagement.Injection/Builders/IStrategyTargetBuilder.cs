namespace DependencyManagement.Injection.Builders;

using Strategies;
using Targets;

public interface IStrategyTargetBuilder<T> : IElementaryTargetBuilder<T> where T : class
{
    ITarget<T> To<TStrategy>() where TStrategy : class, IStrategy;
}