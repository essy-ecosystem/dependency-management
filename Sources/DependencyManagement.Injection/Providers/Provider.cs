namespace DependencyManagement.Providers;

using Components;
using Containers;

public abstract class Provider<T> : Component, IProvider<T> where T : notnull
{
    public abstract T CreateInstance(IReadOnlyContainer container);
}