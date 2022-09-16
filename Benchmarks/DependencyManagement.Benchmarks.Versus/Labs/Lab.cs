namespace DependencyManagement.Benchmarks.Versus.Labs;

public abstract class Lab : ILab, IDisposable
{
    public abstract void Initialize();

    public abstract void Singleton();

    public abstract void Transient();

    public abstract void Dispose();

    protected string JIT(object? obj)
    {
        return obj?.ToString() ?? string.Empty;
    }
}