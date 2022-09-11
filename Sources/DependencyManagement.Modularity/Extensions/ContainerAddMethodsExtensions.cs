namespace DependencyManagement.Modularity.Extensions;

using Builders;
using Composition.Containers;
using Modules;

public static class ContainerAddMethodsExtensions
{
    public static IModuleBuilder<T> AddModule<T>(this IContainer container) where T : class, IModule, new()
    {
        return new ModuleBuilder<T>(container);
    }

    public static void AddModule(this IContainer container, IModule module)
    {
        module.Load(container);
    }
}