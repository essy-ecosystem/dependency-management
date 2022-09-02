namespace DependencyManagement.Generator.Core.Extensions;

using Microsoft.CodeAnalysis;
using Transformers;

public static class CompilationProviderExtensions
{
    public static IncrementalValueProvider<T> Transform<T>(this IncrementalValueProvider<Compilation> provider,
        ICompilationTransformer<T> transformer)
    {
        return transformer.Transform(provider);
    }
}