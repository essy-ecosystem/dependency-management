using DependencyManagement.Composition.Composites;
using DependencyManagement.Composition.Enums;
using DependencyManagement.Composition.Extensions;
using DependencyManagement.Injection.Collections;
using DependencyManagement.Injection.Targets;

namespace DependencyManagement.Injection.Extensions;

public static class CompositeAllMethodsExtensions
{
    public static IReadOnlyList<ITarget<T>> AllTarget<T>(this IReadOnlyComposite composite) where T : class
    {
        return composite.All<ITarget<T>>();
    }

    public static IReadOnlyList<ITarget<T>> AllTarget<T>(this IReadOnlyComposite composite,
        CompositeTraversalStrategy strategy) where T : class
    {
        return composite.All<ITarget<T>>(strategy);
    }

    public static IReadOnlyList<T> AllInstance<T>(this IReadOnlyComposite composite) where T : class
    {
        return new ReadOnlyTargetList<T>(composite, composite.All<ITarget<T>>());
    }

    public static IReadOnlyList<T> AllInstance<T>(this IReadOnlyComposite composite,
        CompositeTraversalStrategy strategy) where T : class
    {
        return new ReadOnlyTargetList<T>(composite, composite.All<ITarget<T>>(strategy));
    }
}