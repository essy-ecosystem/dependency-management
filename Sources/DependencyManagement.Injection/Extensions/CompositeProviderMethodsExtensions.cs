using DependencyManagement.Composition.Composites;
using DependencyManagement.Composition.Extensions;
using DependencyManagement.Composition.Utils;
using DependencyManagement.Injection.Providers;

namespace DependencyManagement.Injection.Extensions;

public static class CompositeProviderMethodsExtensions
{
    public static bool TrySetLazyProvider<T>(this IComposite composite, Func<IProvider<T>> provider) where T : class
    {
        return CompositeTreeUtils.GetLast(composite).TrySetLazy(provider);
    }
}