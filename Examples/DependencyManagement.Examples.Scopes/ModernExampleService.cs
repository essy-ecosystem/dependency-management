namespace DependencyManagement.Examples.Scopes;

public class ModernExampleService : IDisposable
{
    private readonly ExampleService _service;

    public ModernExampleService(ExampleService service)
    {
        _service = service;
    }

    public void Dispose() { }
}