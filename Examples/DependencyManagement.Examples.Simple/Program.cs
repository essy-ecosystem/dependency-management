using DependencyManagement.Composition.Containers;
using DependencyManagement.Composition.Extensions;
using DependencyManagement.Examples.Simple;
using DependencyManagement.Injection.Extensions;
using DependencyManagement.Injection.Providers;

var container = new Container()
    .WithStrategies()
    .WithProviders();

container.SetTarget<ExampleService>()
    .AsSelf().As<IExampleService>()
    .ToSingleton();

container.SetTarget<List<ModernExampleService>>().ToSingleton();

container.SetTarget<ModernExampleService>().ToTransient();

var service = container.LastInstance<ModernExampleService>();

Console.WriteLine(service.GetHashCode());
