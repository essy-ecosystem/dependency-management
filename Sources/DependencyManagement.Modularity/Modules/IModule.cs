namespace DependencyManagement.Modularity.Modules;

using Composition.Components;
using Composition.Containers;

public interface IModule : IComponent
{
    void Load(IContainer container);
}