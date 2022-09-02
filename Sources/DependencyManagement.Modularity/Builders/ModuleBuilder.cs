namespace DependencyManagement.Modularity.Builders;

using Composition.Composites;
using Composition.Extensions;
using Composition.Utils;
using Modules;

public class ModuleBuilder<T> : IModuleBuilder<T> where T : class, IModule, new()
{
    private readonly IComposite _composite;

    private Func<T> _module;

    public ModuleBuilder(IComposite composite)
    {
        _composite = composite;
        _module = () => CompositeTreeUtils.GetLast(composite).LastLazy<T>().Value;
    }

    public IActivationModuleBuilder<T> With(Func<T> module)
    {
        _module = module;
        return this;
    }

    public void ToModule()
    {
        _module().Load(_composite);
    }
}