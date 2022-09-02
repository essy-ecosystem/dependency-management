namespace DependencyManagement.Modularity.Extensions;

using Composition.Containers;
using Composition.Extensions;
using Composition.Utils;
using Modules;

public static class CompositeModuleMethodsExtensions
{
    public static bool TrySetLazyModule<T>(this IContainer container, Func<T> module) where T : class, IModule
    {
        return ContainerTreeUtils.GetLast(container).TrySetLazy(module);
    }
}