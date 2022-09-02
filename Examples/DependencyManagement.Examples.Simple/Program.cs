using DependencyManagement.Composition.Containers;
using DependencyManagement.Examples.Simple;
using DependencyManagement.Injection.Extensions;

var composite = new Container()
    .WithStrategies()
    .WithProviders();

composite.SetTarget<ExampleService>().ToSingleton();
composite.SetTarget<ModernExampleService>().ToSingleton();

var service = composite.LastInstance<ModernExampleService>();

Console.WriteLine(service.GetHashCode());