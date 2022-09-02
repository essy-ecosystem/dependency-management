namespace DependencyManagement.Injection.Providers;

using Composition.Components;
using Composition.Composites;

public abstract class Provider<T> : Component, IProvider<T> where T : notnull
{
    public abstract T GetInstance(IReadOnlyComposite composite);
}