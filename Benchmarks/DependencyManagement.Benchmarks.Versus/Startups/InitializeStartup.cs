namespace DependencyManagement.Benchmarks.Versus.Startups;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;

[SimpleJob(RunStrategy.ColdStart, RuntimeMoniker.Net70)]
[SimpleJob(RunStrategy.ColdStart, RuntimeMoniker.NativeAot70)]
[MemoryDiagnoser]
public class InitializeStartup : Startup
{
    [Benchmark(Description = "Dependency Management - Initialize", Baseline = true)]
    public void RunInitializeDependencyManagement()
    {
        DependencyManagementLab!.Initialize();
    }
    
    [Benchmark(Description = "Microsoft Dependency Injection - Initialize")]
    public void RunInitializeDependencyInjection()
    {
        MicrosoftDependencyInjectionLab!.Initialize();
        
    }
    
    [Benchmark(Description = "Autofac - Initialize")]
    public void RunInitializeAutofac()
    {
        AutofacLab!.Initialize();
        
    }
}