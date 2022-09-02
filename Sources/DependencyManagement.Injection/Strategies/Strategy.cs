namespace DependencyManagement.Injection.Strategies;

using Composition.Components;
using Composition.Composites;
using Providers;

public abstract class Strategy : Component, IStrategy
{
    public abstract T GetInstance<T>(IReadOnlyComposite composite, IProvider<T> provider) where T : notnull;
}