namespace DependencyManagement.Strategies;

using Providers;
using Targets;

public sealed class UnsafeTransientStrategy : Strategy
{
    public override ITarget<T> BuildTarget<T>(IProvider<T> provider)
    {
        return new UnsafeTransientTarget<T>(provider);
    }
}