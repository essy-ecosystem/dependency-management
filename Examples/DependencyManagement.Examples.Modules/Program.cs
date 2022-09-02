using DependencyManagement.Composition.Containers;
using DependencyManagement.Examples.Modules;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Modularity.Extensions;

await using var container = new Container()
    .WithStrategies()
    .WithProviders()
    .WithModules();

container.AddModule<ExampleModule>().ToModule();

var service = container.LastInstance<ModernExampleService>();

Console.WriteLine(service.GetHashCode());