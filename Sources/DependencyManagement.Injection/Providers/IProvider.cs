namespace DependencyManagement.Injection.Providers;

using Composition.Components;
using Composition.Containers;

public interface IProvider<out T> : IProvider where T : notnull
{
    new T CreateInstance(IReadOnlyContainer container);
}

public interface IProvider : IComponent
{
    object CreateInstance(IReadOnlyContainer container);
}
