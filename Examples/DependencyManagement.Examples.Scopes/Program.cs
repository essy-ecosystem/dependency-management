using DependencyManagement.Composition.Containers;
using DependencyManagement.Examples.Scopes;
using DependencyManagement.Injection.Extensions;

await using IContainer applicationContainer = new Container()
    .WithStrategies()
    .WithProviders();

applicationContainer.AddTarget<ExampleService>().ToScope();

for (var i = 0; i < 10; i++)
{
    await using IScope applicationScope = new Scope(applicationContainer);

    var service1 = applicationScope.LastInstance<ExampleService>();
    var service2 = applicationScope.LastInstance<ExampleService>();
    
    Console.WriteLine(service1.GetHashCode());
    Console.WriteLine(service2.GetHashCode());
    Console.WriteLine();
}

