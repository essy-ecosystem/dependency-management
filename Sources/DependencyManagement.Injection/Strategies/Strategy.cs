using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;
using DependencyManagement.Injection.Providers;

namespace DependencyManagement.Injection.Strategies;

public abstract class Strategy : Component, IStrategy
{
    public abstract T GetInstance<T>(IReadOnlyComposite composite, IProvider<T> provider) where T : notnull;
}