namespace DependencyManagement.Benchmarks.Versus.Labs;

using Autofac;
using Models;

public sealed class AutofacLab : Lab
{
    private readonly IContainer? _container;

    public AutofacLab()
    {
        _container = CreateContainer();
    }

    public override void Initialize()
    {
        JIT(CreateContainer());
    }

    public override void Singleton()
    {
        JIT(_container!.Resolve<IService>());
    }

    public override void Transient()
    {
        JIT(_container!.Resolve<IRepository>());
    }

    public override void Dispose()
    {
        _container!.Dispose();
    }

    private IContainer CreateContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<Repository>().As<IRepository>().InstancePerDependency();
        builder.RegisterType<Service>().As<IService>().SingleInstance();
        return builder.Build();
    }
}