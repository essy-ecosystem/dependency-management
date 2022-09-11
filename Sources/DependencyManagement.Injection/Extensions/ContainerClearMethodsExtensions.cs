namespace DependencyManagement.Injection.Extensions;

using Composition.Containers;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class ContainerClearMethodsExtensions
{
    public static void ClearTarget<T>(this IContainer container) where T : class
    {
        container.Clear<ITarget<T>>();
    }

    public static void ClearTarget<T>(this IContainer container, TraversalStrategy strategy) where T : class
    {
        container.Clear<ITarget<T>>(strategy);
    }
}