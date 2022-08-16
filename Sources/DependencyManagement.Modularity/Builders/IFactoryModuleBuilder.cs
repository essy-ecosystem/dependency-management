using DependencyManagement.Modularity.Modules;

namespace DependencyManagement.Modularity.Builders;

public interface IFactoryModuleBuilder<T> : IActivationModuleBuilder<T> where T : class, IModule, new()
{
    IActivationModuleBuilder<T> With(Func<T> module);
}