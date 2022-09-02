namespace DependencyManagement.Composition.Utils;

using Containers;

public static class ContainerTreeUtils
{
    public static IReadOnlyContainer GetLast(IReadOnlyContainer container)
    {
        var rootComposite = container;

        while (rootComposite.Father is not null)
        {
            rootComposite = rootComposite.Father!;
        }

        return rootComposite;
    }

    public static IContainer GetLast(IContainer container)
    {
        var rootComposite = container;

        while (rootComposite.Father is not null)
        {
            rootComposite = rootComposite.Father!;
        }

        return rootComposite;
    }

    public static IReadOnlyContainer GetWhile(IReadOnlyContainer container, Predicate<IReadOnlyContainer> predicate)
    {
        var rootComposite = container;

        while (rootComposite.Father is not null && predicate(container))
        {
            rootComposite = rootComposite.Father!;
        }

        return rootComposite;
    }

    public static IContainer GetWhile(IContainer container, Predicate<IContainer> predicate)
    {
        var rootComposite = container;

        while (rootComposite.Father is not null && predicate(container))
        {
            rootComposite = rootComposite.Father!;
        }

        return rootComposite;
    }

    public static IReadOnlyContainer GetExcept(IReadOnlyContainer container, Predicate<IReadOnlyContainer> predicate)
    {
        var rootComposite = container;

        while (rootComposite.Father is not null && !predicate(container))
        {
            rootComposite = rootComposite.Father!;
        }

        return rootComposite;
    }

    public static IContainer GetExcept(IContainer container, Predicate<IContainer> predicate)
    {
        var rootComposite = container;

        while (rootComposite.Father is not null && !predicate(container))
        {
            rootComposite = rootComposite.Father!;
        }

        return rootComposite;
    }

    public static int GetDeep(IReadOnlyContainer container)
    {
        var deep = 0;

        while (container.Father is not null)
        {
            ++deep;
            container = container.Father;
        }

        return deep;
    }

    public static IReadOnlyList<IReadOnlyContainer> GetTree(IReadOnlyContainer container)
    {
        var composites = new List<IReadOnlyContainer>(2);
        composites.Add(container);

        while (container.Father is not null)
        {
            composites.Add(container.Father);
            container = container.Father;
        }

        return composites;
    }
}