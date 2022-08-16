using DependencyManagement.Modularity.Modules;

namespace DependencyManagement.Modularity.Builders;

public interface IModuleBuilder<T> : IFactoryModuleBuilder<T> where T : class, IModule, new()
{
}