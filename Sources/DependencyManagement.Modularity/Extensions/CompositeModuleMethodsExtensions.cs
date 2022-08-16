using DependencyManagement.Composition.Composites;
using DependencyManagement.Composition.Extensions;
using DependencyManagement.Composition.Utils;
using DependencyManagement.Modularity.Modules;

namespace DependencyManagement.Modularity.Extensions;

public static class CompositeModuleMethodsExtensions
{
    public static bool TrySetLazyModule<T>(this IComposite composite, Func<T> module) where T : class, IModule
    {
        return CompositeTreeUtils.GetLast(composite).TrySetLazy(module);
    }
}