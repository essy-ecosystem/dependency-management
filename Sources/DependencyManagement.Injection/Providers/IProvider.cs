namespace DependencyManagement.Providers;

using Components;
using Containers;

public interface IProvider<out T> : IComponent where T : notnull
{
    T CreateInstance(IReadOnlyContainer container);
}