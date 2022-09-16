namespace DependencyManagement.Benchmarks.Versus.Startups;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;

[SimpleJob(RunStrategy.Throughput, RuntimeMoniker.Net70)]
public class TransientStartup : Startup
{
    [Benchmark(Description = "Dependency Management - Transient", Baseline = true)]
    public void RunTransientDependencyManagement()
    {
        DependencyManagementLab.Transient();
    }
    
    [Benchmark(Description = "Microsoft Dependency Injection - Transient")]
    public void RunTransientDependencyInjection()
    {
        MicrosoftDependencyInjectionLab.Transient();
    }
    
    [Benchmark(Description = "Autofac - Transient")]
    public void RunTransientAutofac()
    {
        AutofacLab.Transient();
    }
}