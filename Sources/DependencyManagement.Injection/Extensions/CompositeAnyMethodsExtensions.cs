using DependencyManagement.Composition.Composites;
using DependencyManagement.Composition.Enums;
using DependencyManagement.Composition.Extensions;
using DependencyManagement.Injection.Targets;

namespace DependencyManagement.Injection.Extensions;

public static class CompositeAnyMethodsExtensions
{
    public static bool AnyTarget<T>(this IReadOnlyComposite composite) where T : class
    {
        return composite.Any<ITarget<T>>();
    }

    public static bool AnyTarget<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class
    {
        return composite.Any<ITarget<T>>(strategy);
    }

    public static bool AnyInstance<T>(this IReadOnlyComposite composite) where T : class
    {
        return composite.AnyTarget<T>();
    }

    public static bool AnyInstance<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy)
        where T : class
    {
        return composite.AnyTarget<T>(strategy);
    }
}