namespace DependencyManagement.Modularity.Builders;

using Modules;

public interface IFactoryModuleBuilder<T> : IActivationModuleBuilder<T> where T : class, IModule, new()
{
    IActivationModuleBuilder<T> With(Func<T> module);
}