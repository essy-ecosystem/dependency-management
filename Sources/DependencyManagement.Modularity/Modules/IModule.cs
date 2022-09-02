namespace DependencyManagement.Modularity.Modules;

using Composition.Components;
using Composition.Composites;

public interface IModule : IComponent
{
    void Load(IComposite composite);
}