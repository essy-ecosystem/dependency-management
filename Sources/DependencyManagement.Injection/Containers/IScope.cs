namespace DependencyManagement.Containers;

public interface IScope : IReadOnlyContainer
{
    IReadOnlyContainer Current { get; }
}