namespace DependencyManagement.Injection.Providers;

using Composition.Components;
using Composition.Containers;

public abstract class Provider<T> : Component, IProvider<T> where T : notnull
{
    public abstract T CreateInstance(IReadOnlyContainer container);
    
    object IProvider.CreateInstance(IReadOnlyContainer container) => CreateInstance(container);
}