namespace DependencyManagement.Composition.Extensions;

using Containers;

public static class ContainerConvertMethodsExtensions
{
    public static IReadOnlyContainer ToReadOnly(this IContainer container)
    {
        return container;
    }
}