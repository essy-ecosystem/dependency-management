namespace DependencyManagement.Injection.Extensions;

using Composition.Composites;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class CompositeClearMethodsExtensions
{
    public static void ClearTarget<T>(this IComposite composite) where T : class
    {
        composite.Clear<ITarget<T>>();
    }

    public static void ClearTarget<T>(this IComposite composite, CompositeTraversalStrategy strategy) where T : class
    {
        composite.Clear<ITarget<T>>(strategy);
    }
}