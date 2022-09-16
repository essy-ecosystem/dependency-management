namespace DependencyManagement.Benchmarks.Versus.Labs;

public interface ILab
{
    void Initialize();

    void Singleton();

    void Transient();

    void Dispose();
}