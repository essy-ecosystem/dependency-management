namespace DependencyManagement.Strategies;

using Providers;
using Targets;

public sealed class TransientStrategy : Strategy
{
    public override ITarget<T> BuildTarget<T>(IProvider<T> provider)
    {
        return new TransientTarget<T>(provider);
    }
}