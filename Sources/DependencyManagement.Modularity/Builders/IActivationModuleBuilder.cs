namespace DependencyManagement.Modularity.Builders;

using Modules;

public interface IActivationModuleBuilder<T> : IElementaryModuleBuilder<T> where T : class, IModule, new()
{
    void ToModule();
}