namespace DependencyManagement.Benchmarks.Versus.Startups;

using BenchmarkDotNet.Attributes;
using Labs;
using Ninject;

public class Startup : IInitializable, IDisposable
{
    protected DependencyManagementLab? DependencyManagementLab;
    
    protected MicrosoftDILab? MicrosoftDependencyInjectionLab;
    
    protected AutofacLab? AutofacLab;

    [GlobalSetup]
    public void Initialize()
    {
        DependencyManagementLab = new();
        MicrosoftDependencyInjectionLab = new();
        AutofacLab = new();
    }

    [GlobalCleanup]
    public void Dispose()
    {
        DependencyManagementLab?.Dispose();
        MicrosoftDependencyInjectionLab?.Dispose();
        AutofacLab?.Dispose();
    }
}