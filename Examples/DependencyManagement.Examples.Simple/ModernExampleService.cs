namespace DependencyManagement.Examples.Simple;

public class ModernExampleService
{
    private readonly ExampleService _service;

    public ModernExampleService(ExampleService service)
    {
        _service = service;
    }
}