namespace DependencyManagement.Generator.Core.Transformers;

using Microsoft.CodeAnalysis;

public sealed class AssemblyNameCompilationTransformer : ICompilationTransformer<string>
{
    public IncrementalValueProvider<string> Transform(IncrementalValueProvider<Compilation> context)
    {
        return context.Select(static (compilation, _) => compilation.Assembly.Name);
    }
}