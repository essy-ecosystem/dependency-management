namespace DependencyManagement.Strategies;

using Providers;
using Targets;

public sealed class SingletonStrategy : Strategy
{
    public override ITarget<T> BuildTarget<T>(IProvider<T> provider)
    {
        return new SingletonTarget<T>(provider);
    }
}