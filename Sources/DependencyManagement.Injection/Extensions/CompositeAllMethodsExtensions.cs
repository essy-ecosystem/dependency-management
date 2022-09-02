namespace DependencyManagement.Injection.Extensions;

using Collections;
using Composition.Composites;
using Composition.Enums;
using Composition.Extensions;
using Targets;

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