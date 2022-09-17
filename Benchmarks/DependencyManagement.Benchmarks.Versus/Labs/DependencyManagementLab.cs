namespace DependencyManagement.Benchmarks.Versus.Labs;

using Composition.Containers;
using Injection.Extensions;
using Models;

public sealed class DependencyManagementLab : Lab
{
    private readonly IContainer? _container;

    public DependencyManagementLab()
    {
        _container = CreateContainer();
    }

    public override void Initialize()
    {
        JIT(CreateContainer());
    }

    public override void Singleton()
    {
        JIT(_container!.FirstInstance<IService>());
    }

    public override void Transient()
    {
        JIT(_container!.FirstInstance<IRepository>());
    }

    public override void Dispose()
    {
        _container!.Dispose();
    }

    private IContainer CreateContainer()
    {
        IContainer container = new Container().WithStrategies().WithProviders();
        container.AddTarget<Repository>().As<IRepository>().ToTransient();
        container.AddTarget<Service>().As<IService>().ToSingleton();
        return container;
    }
}