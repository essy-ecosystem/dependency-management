namespace DependencyManagement.Modularity.Extensions;

using Builders;
using Composition.Composites;
using Modules;

public static class CompositeAddMethodsExtensions
{
    public static IModuleBuilder<T> AddModule<T>(this IComposite composite) where T : class, IModule, new()
    {
        return new ModuleBuilder<T>(composite);
    }

    public static void AddModule(this IComposite composite, IModule module)
    {
        module.Load(composite);
    }
}