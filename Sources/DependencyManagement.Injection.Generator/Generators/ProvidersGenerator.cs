using DependencyManagement.Injection.Generator.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DependencyManagement.Injection.Generator.Generators;

[Generator]
public sealed class ProvidersGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var types = context.SyntaxProvider.CreateSyntaxProvider<(string, string, (string, string)[])?>(
                static (node, _) =>
                    node is InvocationExpressionSyntax invocation
                    && invocation.Expression is MemberAccessExpressionSyntax expression
                    && expression.Name is GenericNameSyntax name
                    && name.Identifier.Text is "AddTarget" or "SetTarget" or "TrySetTarget"
                    && name.TypeArgumentList.Arguments.Count == 1,
                static (context, _) =>
                {
                    var node = (InvocationExpressionSyntax)context.Node;
                    var expression = (MemberAccessExpressionSyntax)node.Expression;
                    var name = (GenericNameSyntax)expression.Name;
                    var argument = name.TypeArgumentList.Arguments.First();
                    var type = context.SemanticModel.GetTypeInfo(argument);
                    if (type.Type is not INamedTypeSymbol namedType) return null;
                    if (namedType.IsAbstract || namedType.TypeKind == TypeKind.Interface) return null;
                    var constructors = namedType.InstanceConstructors;
                    if (constructors.Length == 0) return null;
                    var constructor = constructors.OrderBy(method => method.Parameters.Length).First();
                    return (namedType.ContainingNamespace.ToDisplayString(), namedType.Name,
                        constructor.Parameters.Select(parameter
                                => (parameter.Type.ContainingNamespace.ToDisplayString(), parameter.Type.Name))
                            .ToArray());
                })
            .Where(static provider => provider is not null);

        var output1 = types.Collect();

        context.RegisterSourceOutput(output1, static (context, output) =>
        {
            foreach (var type in output)
            {
                var builder = new ProviderBuilder(type!.Value.Item1, type.Value.Item2, type.Value.Item3);
                context.AddSource($"{type.Value.Item2}GeneratedProvider.ProviderGenerator.cs", builder.ToString());
            }
        });

        var providers = types.Select(static (type, _) => (type!.Value.Item1, type.Value.Item2));

        var assembly = context.CompilationProvider
            .Select(static (compilation, _) => compilation.Assembly.Name);

        var methods = context.CompilationProvider
            .SelectMany(static (compilation, _) => compilation.SourceModule.ReferencedAssemblySymbols)
            .SelectMany(static (assembly, _) => assembly.GlobalNamespace.GetNamespaceMembers())
            .Where(static @namespace => @namespace.Name == "DependencyManagement")
            .SelectMany(static (@namespace, _) => @namespace.GetNamespaceMembers())
            .Where(static @namespace => @namespace.Name == "Injection")
            .SelectMany(static (@namespace, _) => @namespace.GetNamespaceMembers())
            .Where(static @namespace => @namespace.Name == "Extensions")
            .SelectMany(static (@namespace, _) => @namespace.GetTypeMembers())
            .Where(static type => type.IsStatic
                                  && type.TypeKind == TypeKind.Class
                                  && type.Name.EndsWith("AssemblyGeneratedCompositeExtensions"))
            .SelectMany(static (type, _) => type.GetMembers())
            .Where(static member => member.IsStatic
                                    && member.Kind == SymbolKind.Method
                                    && member.Name.StartsWith("WithProvidersFrom"))
            .Select(static (method, _) => method.Name);

        var output2 = assembly.Combine(methods.Collect()).Combine(providers.Collect());

        context.RegisterSourceOutput(output2, static (context, output) =>
        {
            var builder =
                new CompositeExtensionsBuilder(output.Left.Left, output.Right.ToArray(), output.Left.Right.ToArray());
            context.AddSource("CompositeExtensions.ProvidersGenerator.cs", builder.ToString());
        });
    }
}