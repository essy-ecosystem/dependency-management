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
    public void RunManagement()
    {
        var container = new Container().WithStrategies().WithProviders();
        container.AddTarget<Service>().ToSingleton();
        var service = container.LastInstance<Service>();
    }
    
    [Benchmark(Description = "MSDependencyInjection")]
    public void RunDependencyInjection()
    {
        var provider = new ServiceCollection().AddSingleton<Service>().BuildServiceProvider();
        var service = provider.GetService<Service>();
    }
    
    [Benchmark(Description = "Autofac")]
    public void RunAutofac()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<Service>().SingleInstance();
        var container = builder.Build();
        var service = container.Resolve<Service>();
    }
    
    [Benchmark(Description = "Ninject")]
    public void RunNinject()
    {
        var kernel = new Ninject.StandardKernel();
        kernel.Bind<Service>().ToSelf().InSingletonScope();
        var service = kernel.Get<Service>();
    }
}