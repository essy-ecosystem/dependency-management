using DependencyManagement.Composition.Composites;

namespace DependencyManagement.Composition.Extensions;

public static class CompositeConvertMethodsExtensions
{
    public static IReadOnlyComposite ToReadOnly(this IComposite composite)
    {
        return composite;
    }
}