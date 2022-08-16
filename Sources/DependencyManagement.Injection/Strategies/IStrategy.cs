using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;
using DependencyManagement.Injection.Providers;

namespace DependencyManagement.Injection.Strategies;

public interface IStrategy : IComponent
{
    T GetInstance<T>(IReadOnlyComposite composite, IProvider<T> provider) where T : notnull;
}