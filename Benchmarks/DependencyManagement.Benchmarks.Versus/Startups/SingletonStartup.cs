namespace DependencyManagement.Benchmarks.Versus.Startups;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;

[SimpleJob(RunStrategy.Throughput, RuntimeMoniker.Net70)]
public class SingletonStartup : Startup
{
    [Benchmark(Description = "Dependency Management - Singleton", Baseline = true)]
    public void RunSingletonDependencyManagement()
    {
        DependencyManagementLab.Singleton();
    }
    
    [Benchmark(Description = "Microsoft Dependency Injection - Singleton")]
    public void RunSingletonDependencyInjection()
    {
        MicrosoftDependencyInjectionLab.Singleton();
    }
    
    [Benchmark(Description = "Autofac - Singleton")]
    public void RunSingletonAutofac()
    {
        AutofacLab.Singleton();
    }
}