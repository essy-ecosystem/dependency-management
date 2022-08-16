using DependencyManagement.Composition.Composites;
using DependencyManagement.Composition.Enums;
using DependencyManagement.Composition.Extensions;
using DependencyManagement.Injection.Targets;

namespace DependencyManagement.Injection.Extensions;

public static class CompositeClearMethodsExtensions
{
    public static void ClearTarget<T>(this IComposite composite) where T : class
    {
        composite.Clear<ITarget<T>>();
    }

    public static void ClearTarget<T>(this IComposite composite, CompositeTraversalStrategy strategy) where T : class
    {
        composite.Clear<ITarget<T>>(strategy);
    }
}