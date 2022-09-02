namespace DependencyManagement.Examples.Modules;

public class ModernExampleService
{
    private readonly ExampleService _service;

    public ModernExampleService(ExampleService service)
    {
        _service = service;
    }
}