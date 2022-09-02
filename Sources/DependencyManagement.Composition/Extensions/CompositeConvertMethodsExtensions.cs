namespace DependencyManagement.Composition.Extensions;

using Composites;

public static class CompositeConvertMethodsExtensions
{
    public static IReadOnlyComposite ToReadOnly(this IComposite composite)
    {
        return composite;
    }
}