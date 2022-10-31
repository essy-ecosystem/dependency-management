namespace DependencyManagement.Builders;

public interface IAliasTargetBuilder<T> : IProviderTargetBuilder<T> where T : notnull
{
    IAliasTargetBuilder<T> As<TA, TIml>() where TIml : TA where TA : T;
}