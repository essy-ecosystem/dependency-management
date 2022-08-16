namespace DependencyManagement.Injection.Builders;

public interface IAbstractionTargetBuilder<T> : IProviderTargetBuilder<T> where T : class
{
    IAbstractionTargetBuilder<T> As<TAs>() where TAs : class;
}