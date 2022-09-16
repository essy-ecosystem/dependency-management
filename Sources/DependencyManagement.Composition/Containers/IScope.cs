namespace DependencyManagement.Composition.Containers;

public interface IScope : IReadOnlyContainer
{
    IReadOnlyContainer Current { get; }
}