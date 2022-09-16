namespace DependencyManagement.Examples.Scopes;

public class ExampleService
{
    public ExampleService()
    {
        Id = Random.Shared.Next();
    }
    public int Id { get; }
}