namespace DependencyManagement.Benchmarks.Versus.Models;

public interface IService
{
    public IRepository Repository { get; }

    void Start();
}