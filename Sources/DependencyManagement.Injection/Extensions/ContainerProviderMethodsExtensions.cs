namespace DependencyManagement.Injection.Extensions;

using Composition.Containers;
using Composition.Extensions;
using Composition.Utils;
using Providers;

public static class ContainerProviderMethodsExtensions
{
    public static bool TrySetLazyProvider<T>(this IContainer container, Func<IProvider<T>> provider) where T : class
    {
        return TraversalService.GetInitial(container).TrySetLazy(provider);
    }
}