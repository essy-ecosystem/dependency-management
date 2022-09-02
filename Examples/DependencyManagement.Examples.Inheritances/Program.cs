using DependencyManagement.Composition.Containers;
using DependencyManagement.Composition.Enums;
using DependencyManagement.Examples.Inheritances;
using DependencyManagement.Injection.Extensions;

await using var applicationContainer = new Container()
    .WithStrategies()
    .WithProviders();

applicationContainer.SetTarget<ModernExampleService>().ToContainer();

while (true)
{
    await using var frameContainer = new Container(applicationContainer);
    
    frameContainer.AddTarget<ExampleService>().ToSingleton();
    
    var service = frameContainer.LastInstance<ModernExampleService>(CompositeTraversalStrategy.Inherit);
    
    Console.WriteLine(service.GetHashCode());
}