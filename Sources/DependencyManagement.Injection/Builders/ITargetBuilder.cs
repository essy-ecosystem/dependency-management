namespace DependencyManagement.Builders;

public interface ITargetBuilder<T> : IAliasTargetBuilder<T> where T : notnull { }