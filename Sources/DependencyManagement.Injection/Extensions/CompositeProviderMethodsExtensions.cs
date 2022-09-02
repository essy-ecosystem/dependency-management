namespace DependencyManagement.Injection.Extensions;

using Composition.Composites;
using Composition.Extensions;
using Composition.Utils;
using Providers;

public static class CompositeProviderMethodsExtensions
{
    public static bool TrySetLazyProvider<T>(this IComposite composite, Func<IProvider<T>> provider) where T : class
    {
        return CompositeTreeUtils.GetLast(composite).TrySetLazy(provider);
    }
}