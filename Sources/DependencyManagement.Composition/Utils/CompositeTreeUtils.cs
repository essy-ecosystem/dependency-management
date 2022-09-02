namespace DependencyManagement.Composition.Utils;

using Composites;

public static class CompositeTreeUtils
{
    public static IReadOnlyComposite GetLast(IReadOnlyComposite composite)
    {
        var rootComposite = composite;

        while (rootComposite.Father is not null)
        {
            rootComposite = rootComposite.Father!;
        }

        return rootComposite;
    }

    public static IComposite GetLast(IComposite composite)
    {
        var rootComposite = composite;

        while (rootComposite.Father is not null)
        {
            rootComposite = rootComposite.Father!;
        }

        return rootComposite;
    }

    public static IReadOnlyComposite GetWhile(IReadOnlyComposite composite, Predicate<IReadOnlyComposite> predicate)
    {
        var rootComposite = composite;

        while (rootComposite.Father is not null && predicate(composite))
        {
            rootComposite = rootComposite.Father!;
        }

        return rootComposite;
    }

    public static IComposite GetWhile(IComposite composite, Predicate<IComposite> predicate)
    {
        var rootComposite = composite;

        while (rootComposite.Father is not null && predicate(composite))
        {
            rootComposite = rootComposite.Father!;
        }

        return rootComposite;
    }

    public static IReadOnlyComposite GetExcept(IReadOnlyComposite composite, Predicate<IReadOnlyComposite> predicate)
    {
        var rootComposite = composite;

        while (rootComposite.Father is not null && !predicate(composite))
        {
            rootComposite = rootComposite.Father!;
        }

        return rootComposite;
    }

    public static IComposite GetExcept(IComposite composite, Predicate<IComposite> predicate)
    {
        var rootComposite = composite;

        while (rootComposite.Father is not null && !predicate(composite))
        {
            rootComposite = rootComposite.Father!;
        }

        return rootComposite;
    }

    public static int GetDeep(IReadOnlyComposite composite)
    {
        var deep = 0;

        while (composite.Father is not null)
        {
            ++deep;
            composite = composite.Father;
        }

        return deep;
    }

    public static IReadOnlyList<IReadOnlyComposite> GetTree(IReadOnlyComposite composite)
    {
        var composites = new List<IReadOnlyComposite>(2);
        composites.Add(composite);

        while (composite.Father is not null)
        {
            composites.Add(composite.Father);
            composite = composite.Father;
        }

        return composites;
    }
}