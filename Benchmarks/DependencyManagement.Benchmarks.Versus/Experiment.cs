namespace DependencyManagement.Benchmarks.Versus;

using Autofac;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using Composition.Containers;
using Injection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Ninject;

[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.NativeAot70)]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class Experiment
{
    [Benchmark(Description = "DependencyManagement")]
    public Service RunManagement()
    {
        using var container = new Container().WithStrategies().WithProviders();
        container.AddTarget<Service>().ToSingleton();
        return container.LastInstance<Service>();
    }
    
    [Benchmark(Description = "MSDependencyInjection")]
    public Service RunDependencyInjection()
    {
        using var provider = new ServiceCollection().AddSingleton<Service>().BuildServiceProvider();
        return provider.GetRequiredService<Service>();
    }
    
    [Benchmark(Description = "Autofac")]
    public Service RunAutofac()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<Service>().SingleInstance();
        using var container = builder.Build();
        return container.Resolve<Service>();
    }
    
    [Benchmark(Description = "Ninject")]
    public Service RunNinject()
    {
        using var kernel = new StandardKernel();
        kernel.Bind<Service>().ToSelf().InSingletonScope();
        return kernel.Get<Service>();
    }
}