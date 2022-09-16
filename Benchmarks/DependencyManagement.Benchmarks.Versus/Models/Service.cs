namespace DependencyManagement.Benchmarks.Versus.Models;

public sealed class Service : IService
{
    public Service(IRepository repository)
    {
        Repository = repository;
    }
    
    public IRepository Repository { get; }
    
    public void Start()
    {
        Repository.Initialize();
    }
}