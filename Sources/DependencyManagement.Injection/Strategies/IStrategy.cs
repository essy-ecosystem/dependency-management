namespace DependencyManagement.Strategies;

using Components;
using Providers;
using Targets;

public interface IStrategy : IComponent
{
    ITarget<T> BuildTarget<T>(IProvider<T> provider) where T : notnull;
}