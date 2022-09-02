namespace DependencyManagement.Modularity.Builders;

using Composition.Containers;
using Composition.Extensions;
using Composition.Utils;
using Modules;

public class ModuleBuilder<T> : IModuleBuilder<T> where T : class, IModule, new()
{
    private readonly IContainer _container;

    private Func<T> _module;

    public ModuleBuilder(IContainer container)
    {
        _container = container;
        _module = () => ContainerTreeUtils.GetLast(container).LastLazy<T>().Value;
    }

    public IActivationModuleBuilder<T> With(Func<T> module)
    {
        _module = module;
        return this;
    }

    public void ToModule()
    {
        _module().Load(_container);
    }
}