using DependencyManagement.Composition.Containers;
using DependencyManagement.Composition.Extensions;
using DependencyManagement.Examples.Scopes;
using DependencyManagement.Injection.Extensions;

await using IContainer applicationContainer = new Container()
    .WithStrategies()
    .WithProviders();

applicationContainer.AddTarget<ExampleService>().ToScope();

while (true)
{
    await using IScope applicationScope = new Scope(applicationContainer);

    var service = applicationScope.LastInstance<ExampleService>();
    
    Console.WriteLine(service.Id);
}

