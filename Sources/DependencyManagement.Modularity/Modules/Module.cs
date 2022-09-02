namespace DependencyManagement.Modularity.Modules;

using Composition.Components;
using Composition.Containers;

public abstract class Module : Component, IModule
{
    public abstract void Load(IContainer container);
}