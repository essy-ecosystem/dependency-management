using DependencyManagement.Composition.Containers;
using DependencyManagement.Composition.Enums;
using DependencyManagement.Examples.Scopes;
using DependencyManagement.Injection.Extensions;

await using var applicationContainer = new Container()
    .WithStrategies()
    .WithProviders();

applicationContainer.SetTarget<ModernExampleService>().ToScope();

while (true)
{
    await using var frameContainer = new Container(applicationContainer);
    
    frameContainer.AddTarget<ExampleService>().ToSingleton();
    
    using var service = frameContainer.LastInstance<ModernExampleService>(TraversalStrategy.Inherit);
    
    Console.WriteLine(service.GetHashCode());
}