namespace DependencyManagement.Modularity.Modules;

using Composition.Components;
using Composition.Composites;

public abstract class Module : Component, IModule
{
    public abstract void Load(IComposite composite);
}