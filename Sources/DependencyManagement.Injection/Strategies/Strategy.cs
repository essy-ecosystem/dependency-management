namespace DependencyManagement.Injection.Strategies;

using Composition.Components;
using Composition.Containers;
using Providers;

public abstract class Strategy : Component, IStrategy
{
    public abstract T GetInstance<T>(IReadOnlyContainer container, IProvider<T> provider) where T : notnull;
}