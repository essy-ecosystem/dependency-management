namespace DependencyManagement.Modularity.Builders;

using Modules;

public interface IModuleBuilder<T> : IFactoryModuleBuilder<T> where T : class, IModule, new() { }