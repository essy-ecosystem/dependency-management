namespace DependencyManagement.Benchmarks.Versus.Labs;

using Microsoft.Extensions.DependencyInjection;
using Models;

public sealed class MicrosoftDILab : Lab
{
    private readonly ServiceProvider? _provider;

    public MicrosoftDILab()
    {
        _provider = CreateProvider();
    }
    
    public override void Initialize()
    {
        JIT(CreateProvider());
    }

    public override void Singleton()
    {
        JIT(_provider!.GetRequiredService<IService>());
    }

    public override void Transient()
    {
        JIT(_provider!.GetRequiredService<IRepository>());
    }

    public override void Dispose()
    {
        _provider!.Dispose();
    }

    private ServiceProvider CreateProvider()
    {
        return new ServiceCollection()
            .AddTransient<IRepository, Repository>()
            .AddSingleton<IService, Service>()
            .BuildServiceProvider();
    }
}