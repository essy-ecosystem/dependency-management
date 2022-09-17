using DependencyManagement.Composition.Containers;
using DependencyManagement.Composition.Enums;
using DependencyManagement.Examples.Simple;
using DependencyManagement.Injection.Extensions;

var container = new Container()
    .WithStrategies()
    .WithProviders();

container.SetTarget<ExampleService>()
    .AsSelf().As<IExampleService>()
    .ToSingleton();

container.SetTarget<ModernExampleService>().ToTransient();

var service = container.LastInstance<ModernExampleService>();

Console.WriteLine(service.GetHashCode());
