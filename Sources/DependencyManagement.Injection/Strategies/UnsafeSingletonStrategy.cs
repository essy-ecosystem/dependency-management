namespace DependencyManagement.Strategies;

using Providers;
using Targets;

public sealed class UnsafeSingletonStrategy : Strategy
{
    public override ITarget<T> BuildTarget<T>(IProvider<T> provider)
    {
        return new UnsafeSingletonTarget<T>(provider);
    }
}