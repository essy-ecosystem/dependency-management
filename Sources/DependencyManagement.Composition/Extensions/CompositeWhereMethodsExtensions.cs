namespace DependencyManagement.Composition.Extensions;

using Components;
using Composites;
using Enums;

public static class CompositeWhereMethodsExtensions
{
    public static IReadOnlyList<T> Where<T>(this IReadOnlyComposite composite, Predicate<T> predicate)
        where T : class, IComponent
    {
        return composite.All<T>().Where(component => predicate(component)).ToArray();
    }

    public static IReadOnlyList<T> Where<T>(this IReadOnlyComposite composite, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current) return composite.Where(predicate);

        var components = new List<T>(composite.Where(predicate));

        while (composite.Father is not null)
        {
            composite = composite.Father!;
            components.AddRange(composite.Where(predicate));
        }

        return components;
    }

    public static IReadOnlyList<ILazyComponent<T>> WhereLazy<T>(this IReadOnlyComposite composite,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return composite.Where(predicate);
    }

    public static IReadOnlyList<ILazyComponent<T>> WhereLazy<T>(this IReadOnlyComposite composite,
        CompositeTraversalStrategy strategy, Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return composite.Where(strategy, predicate);
    }
}