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
        JIT(_container!.LastInstance<IService>());
    }

    public override void Transient()
    {
        JIT(_container!.LastInstance<IRepository>());
    }

    public override void Dispose()
    {
        _container!.Dispose();
    }

    private IContainer CreateContainer()
    {
        IContainer container = new Container().WithStrategies().WithProviders();
        container.AddTarget<Repository>().As<IRepository>().With(_ => new()).ToTransient();
        container.AddTarget<Service>().As<IService>().With(c => new(c.LastInstance<IRepository>())).ToSingleton();
        return container;
    }
}