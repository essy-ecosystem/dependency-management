namespace DependencyManagement.Generator.Core.Transformers;

using Microsoft.CodeAnalysis;

public interface ISyntaxTransformer<out T>
{
    T Transform(GeneratorSyntaxContext context);
}