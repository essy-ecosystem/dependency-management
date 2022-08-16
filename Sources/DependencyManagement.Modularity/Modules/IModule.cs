using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;

namespace DependencyManagement.Modularity.Modules;

public interface IModule : IComponent
{
    void Load(IComposite composite);
}