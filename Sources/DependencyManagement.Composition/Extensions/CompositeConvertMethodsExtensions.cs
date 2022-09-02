namespace DependencyManagement.Composition.Extensions;

using Containers;

public static class CompositeConvertMethodsExtensions
{
    public static IReadOnlyContainer ToReadOnly(this IContainer container)
    {
        return container;
    }
}