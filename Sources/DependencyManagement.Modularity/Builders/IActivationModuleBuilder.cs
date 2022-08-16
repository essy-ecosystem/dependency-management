using DependencyManagement.Modularity.Modules;

namespace DependencyManagement.Modularity.Builders;

public interface IActivationModuleBuilder<T> : IElementaryModuleBuilder<T> where T : class, IModule, new()
{
    void ToModule();
}