namespace DependencyManagement.Examples.Simple;

using Core.Disposables;

public class ModernExampleService : IDisposable
{
    private readonly IExampleService _service;

    public ModernExampleService(IExampleService service)
    {
        _service = service;
    }

    public void Dispose() { }
}