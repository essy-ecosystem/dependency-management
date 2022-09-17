namespace DependencyManagement.Benchmarks.Versus.Startups;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;

[SimpleJob(RunStrategy.ColdStart, RuntimeMoniker.Net70)]
[SimpleJob(RunStrategy.ColdStart, RuntimeMoniker.NativeAot70)]
[MemoryDiagnoser]
public class DisposeStartup : Startup
{
    [Benchmark(Description = "Dependency Management - Dispose", Baseline = true)]
    public void RunDisposeDependencyManagement()
    {
        DependencyManagementLab!.Dispose();
    }
    
    [Benchmark(Description = "Microsoft Dependency Injection - Dispose")]
    public void RunDisposeDependencyInjection()
    {
        MicrosoftDependencyInjectionLab!.Dispose();
    }
    
    [Benchmark(Description = "Autofac - Dispose")]
    public void RunDisposeAutofac()
    {
        AutofacLab!.Dispose();
    }
}