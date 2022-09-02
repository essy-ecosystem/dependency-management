namespace DependencyManagement.Composition.Extensions;

using Components;
using Containers;
using Enums;
using Utils;

public static class CompositeWhereMethodsExtensions
{
    public static IReadOnlyList<T> Where<T>(this IReadOnlyContainer container, Predicate<T> predicate)
        where T : class, IComponent
    {
        return container.All<T>().Where(component => predicate(component)).ToArray();
    }

    public static IReadOnlyList<T> Where<T>(this IReadOnlyContainer container, CompositeTraversalStrategy strategy,
        Predicate<T> predicate) where T : class, IComponent
    {
        if (strategy == CompositeTraversalStrategy.Current) return container.Where(predicate);
        if (strategy == CompositeTraversalStrategy.Initial) return ContainerTreeUtils.GetLast(container).Where(predicate);

        var components = new List<T>(container.Where(predicate));

        while (container.Father is not null)
        {
            container = container.Father!;
            components.AddRange(container.Where(predicate));
        }

        return components;
    }

    public static IReadOnlyList<ILazyComponent<T>> WhereLazy<T>(this IReadOnlyContainer container,
        Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return container.Where(predicate);
    }

    public static IReadOnlyList<ILazyComponent<T>> WhereLazy<T>(this IReadOnlyContainer container,
        CompositeTraversalStrategy strategy, Predicate<ILazyComponent<T>> predicate) where T : class, IComponent
    {
        return container.Where(strategy, predicate);
    }
}