namespace DependencyManagement.Examples.Modules;

using Composition.Containers;
using Injection.Extensions;
using Modularity.Modules;

public sealed class ExampleModule : Module
{
    public override void Load(IContainer container)
    {
        container.SetTarget<ExampleService>().ToSingleton();
        container.SetTarget<ModernExampleService>().ToSingleton();
    }
}