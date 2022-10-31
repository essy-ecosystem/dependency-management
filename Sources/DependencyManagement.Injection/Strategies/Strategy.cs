namespace DependencyManagement.Strategies;

using Components;
using Providers;
using Targets;

public abstract class Strategy : Component, IStrategy
{
    public abstract ITarget<T> BuildTarget<T>(IProvider<T> provider) where T : notnull;
}