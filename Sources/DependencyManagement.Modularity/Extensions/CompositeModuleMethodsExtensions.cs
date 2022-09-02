namespace DependencyManagement.Modularity.Extensions;

using Composition.Composites;
using Composition.Extensions;
using Composition.Utils;
using Modules;

public static class CompositeModuleMethodsExtensions
{
    public static bool TrySetLazyModule<T>(this IComposite composite, Func<T> module) where T : class, IModule
    {
        return CompositeTreeUtils.GetLast(composite).TrySetLazy(module);
    }
}