namespace DependencyManagement.Generator.Core.Extensions;

using Discovers;
using Microsoft.CodeAnalysis;
using Transformers;

public static class SyntaxValueProviderExtensions
{
    public static IncrementalValuesProvider<T> Transform<T>(this SyntaxValueProvider provider, ISyntaxDiscover discover,
        ISyntaxTransformer<T> transformer)
    {
        return provider.CreateSyntaxProvider((node, _) => discover.Match(node),
            (context, _) => transformer.Transform(context));
    }
}