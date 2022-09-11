namespace DependencyManagement.Composition.Utils;

using Containers;

public static class TraversalService
{
    public static IReadOnlyContainer GetInitial(IReadOnlyContainer container)
    {
        var rootContainer = container;

        while (rootContainer.Father is not null)
        {
            rootContainer = rootContainer.Father!;
        }

        return rootContainer;
    }

    public static IContainer GetInitial(IContainer container)
    {
        return (IContainer)GetInitial((IReadOnlyContainer)container);
    }

    public static IReadOnlyList<IReadOnlyContainer> GetInherit(IReadOnlyContainer container)
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
    
    public static IReadOnlyList<IContainer> GetInherit(IContainer container)
    {
        return GetInherit((IReadOnlyContainer)container).Cast<IContainer>().ToArray();
    }
}