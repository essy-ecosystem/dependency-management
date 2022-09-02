namespace DependencyManagement.Injection.Extensions;

using Builders;
using Composition.Composites;
using Composition.Enums;
using Composition.Extensions;
using Targets;

public static class CompositeAddMethodsExtensions
{
    public static ITargetBuilder<T> AddTarget<T>(this IComposite composite) where T : class
    {
        return new TargetBuilder<T>(composite);
    }

    public static void AddTarget<T>(this IComposite composite, ITarget<T> target) where T : class
    {
        composite.Add(target);
    }

    public static bool TryAddTarget<T>(this IComposite composite, ITarget<T> target) where T : class
    {
        return composite.TryAdd(target);
    }

    public static bool TryAddTarget<T>(this IComposite composite, ITarget<T> target,
        CompositeTraversalStrategy strategy) where T : class
    {
        return composite.TryAdd(target, strategy);
    }
}