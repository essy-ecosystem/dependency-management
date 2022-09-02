namespace DependencyManagement.Generator.Core.Transformers;

using Microsoft.CodeAnalysis;

public interface ICompilationTransformer<T>
{
    IncrementalValueProvider<T> Transform(IncrementalValueProvider<Compilation> context);
}