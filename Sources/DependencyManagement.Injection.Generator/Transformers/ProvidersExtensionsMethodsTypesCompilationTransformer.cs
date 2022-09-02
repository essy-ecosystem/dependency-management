namespace DependencyManagement.Injection.Generator.Transformers;

using System.Collections.Immutable;
using DependencyManagement.Generator.Core.Models;
using DependencyManagement.Generator.Core.Transformers;
using Microsoft.CodeAnalysis;

public sealed class
    ProvidersExtensionsMethodsTypesCompilationTransformer : ICompilationTransformer<ImmutableArray<TypeModel>>
{
    private const string Namespace1 = "DependencyManagement";

    private const string Namespace2 = "Injection";

    private const string Namespace3 = "Extensions";

    private const string GeneratedExtensionTypePostfixName = "AssemblyGeneratedCompositeExtensions";

    private const string GeneratedExtensionMethodPrefixName = "WithProvidersFrom";

    public IncrementalValueProvider<ImmutableArray<TypeModel>> Transform(IncrementalValueProvider<Compilation> context)
    {
        var types = GetReferencedAssemblyTypesInNamespace(context,
            Namespace1, Namespace2, Namespace3);
        var members = GetGeneratedCompositeExtensionsMembers(types);
        var methods = GetProvidersReferencedAssemblyExtensionsMethods(members);

        return methods
            .Select((method, _) => GetMethod(method))
            .Collect();
    }

    private TypeModel GetMethod(ISymbol parameter)
    {
        var ns = parameter.ContainingNamespace.ToDisplayString();
        var name = parameter.Name;

        return new TypeModel(ns, name);
    }

    private IncrementalValuesProvider<INamedTypeSymbol> GetReferencedAssemblyTypesInNamespace(
        IncrementalValueProvider<Compilation> context, params string[] ns)
    {
        var rootNamespaces = context
            .SelectMany(static (compilation, _) => compilation.SourceModule.ReferencedAssemblySymbols)
            .SelectMany(static (assembly, _) => assembly.GlobalNamespace.GetNamespaceMembers());

        var lastNamespaces = rootNamespaces;

        for (var i = 0; i < ns.Length - 1; i++)
        {
            var currentNamespaceName = ns[i];

            lastNamespaces = lastNamespaces
                .Where(@namespace => @namespace.Name == currentNamespaceName)
                .SelectMany(static (@namespace, _) => @namespace.GetNamespaceMembers());
        }

        return lastNamespaces.SelectMany(static (@namespace, _) => @namespace.GetTypeMembers());
    }

    private IncrementalValuesProvider<ISymbol> GetGeneratedCompositeExtensionsMembers(
        IncrementalValuesProvider<INamedTypeSymbol> types)
    {
        return types.Where(static type => type.IsStatic
                && type.TypeKind == TypeKind.Class
                && type.Name.EndsWith(GeneratedExtensionTypePostfixName))
            .SelectMany(static (type, _) => type.GetMembers());
    }

    private IncrementalValuesProvider<ISymbol> GetProvidersReferencedAssemblyExtensionsMethods(
        IncrementalValuesProvider<ISymbol> members)
    {
        return members.Where(static member => member.IsStatic
            && member.Kind == SymbolKind.Method
            && member.Name.StartsWith(GeneratedExtensionMethodPrefixName));
    }
}