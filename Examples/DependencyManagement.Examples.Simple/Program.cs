using DependencyManagement.Composition.Composites;
using DependencyManagement.Examples.Simple;
using DependencyManagement.Injection.Extensions;

var composite = new Composite()
    .WithStrategies()
    .WithProviders();

composite.SetTarget<ExampleService>().ToSingleton();
composite.SetTarget<ModernExampleService>().ToSingleton();

var service = composite.LastInstance<ModernExampleService>();

Console.WriteLine(service.GetHashCode());