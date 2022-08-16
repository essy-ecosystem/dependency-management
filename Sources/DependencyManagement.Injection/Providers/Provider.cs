using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;

namespace DependencyManagement.Injection.Providers;

public abstract class Provider<T> : Component, IProvider<T> where T : notnull
{
    public abstract T GetInstance(IReadOnlyComposite composite);
}