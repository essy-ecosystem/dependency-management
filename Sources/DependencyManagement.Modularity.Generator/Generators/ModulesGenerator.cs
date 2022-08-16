using DependencyManagement.Modularity.Generator.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DependencyManagement.Injection.Generator.Generators;

[Generator]
public sealed class ModulesGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var types = context.SyntaxProvider.CreateSyntaxProvider<(string, string)?>(
                static (node, _) =>
                    node is InvocationExpressionSyntax invocation
                    && invocation.Expression is MemberAccessExpressionSyntax expression
                    && expression.Name is GenericNameSyntax name
                    && name.Identifier.Text is "AddModule"
                    && name.TypeArgumentList.Arguments.Count == 1,
                static (context, _) =>
                {
                    var node = (InvocationExpressionSyntax)context.Node;
                    var symbol = context.SemanticModel.GetSymbolInfo(node).Symbol as IMethodSymbol;
                    var expression = (MemberAccessExpressionSyntax)node.Expression;
                    var name = (GenericNameSyntax)expression.Name;
                    var argument = name.TypeArgumentList.Arguments.First();
                    var type = context.SemanticModel.GetTypeInfo(argument);
                    if (type.Type is not INamedTypeSymbol namedType) return null;
                    if (namedType.IsAbstract || namedType.TypeKind == TypeKind.Interface) return null;
                    return (namedType.ContainingNamespace.ToDisplayString(), namedType.Name);
                })
            .Where(static module => module is not null);

        var assembly = context.CompilationProvider
            .Select(static (compilation, _) => compilation.Assembly.Name);

        var methods = context.CompilationProvider
            .SelectMany(static (compilation, _) => compilation.SourceModule.ReferencedAssemblySymbols)
            .SelectMany(static (assembly, _) => assembly.GlobalNamespace.GetNamespaceMembers())
            .Where(static @namespace => @namespace.Name == "DependencyManagement")
            .SelectMany(static (@namespace, _) => @namespace.GetNamespaceMembers())
            .Where(static @namespace => @namespace.Name == "Modularity")
            .SelectMany(static (@namespace, _) => @namespace.GetNamespaceMembers())
            .Where(static @namespace => @namespace.Name == "Extensions")
            .SelectMany(static (@namespace, _) => @namespace.GetTypeMembers())
            .Where(static type => type.IsStatic
                                  && type.TypeKind == TypeKind.Class
                                  && type.Name.EndsWith("AssemblyGeneratedCompositeExtensions"))
            .SelectMany(static (type, _) => type.GetMembers())
            .Where(static member => member.IsStatic
                                    && member.Kind == SymbolKind.Method
                                    && member.Name.StartsWith("WithModulesFrom"))
            .Select(static (method, _) => method.Name);

        var modules = types.Select(static (module, _) => module!.Value);

        var output = assembly.Combine(methods.Collect()).Combine(modules.Collect());

        context.RegisterSourceOutput(output, static (context, output) =>
        {
            var builder =
                new CompositeExtensionsBuilder(output.Left.Left, output.Right.ToArray(), output.Left.Right.ToArray());
            context.AddSource("CompositeExtensions.ModulesGenerator.cs", builder.ToString());
        });
    }
}