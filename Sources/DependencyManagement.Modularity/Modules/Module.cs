using DependencyManagement.Composition.Components;
using DependencyManagement.Composition.Composites;

namespace DependencyManagement.Modularity.Modules;

public abstract class Module : Component, IModule
{
    public abstract void Load(IComposite composite);
}