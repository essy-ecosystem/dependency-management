namespace DependencyManagement.Modularity.Generator.Transformers;

using DependencyManagement.Generator.Core.Models;
using DependencyManagement.Generator.Core.Transformers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public sealed class ModulesRegistrationTypesSyntaxTransformer : ISyntaxTransformer<TypeModel?>
{
    public TypeModel? Transform(GeneratorSyntaxContext context)
    {
        var providedType = GetModuleTypeFromGeneric(context);

        return ValidateProvidedType(providedType)
            ? GetModuleTypeModel(providedType)
            : null;
    }

    private TypeModel GetModuleTypeModel(INamedTypeSymbol providedType)
    {
        var ns = providedType.ContainingNamespace.ToDisplayString();
        var name = providedType.Name;

        return new TypeModel(ns, name);
    }

    private bool ValidateProvidedType(INamedTypeSymbol providedType)
    {
        return !providedType.IsAbstract && providedType.TypeKind is TypeKind.Class;
    }

    private INamedTypeSymbol GetModuleTypeFromGeneric(GeneratorSyntaxContext context)
    {
        var invocationExpression = (InvocationExpressionSyntax)context.Node;
        var memberAccessExpression = (MemberAccessExpressionSyntax)invocationExpression.Expression;

        var genericName = (GenericNameSyntax)memberAccessExpression.Name;
        var argumentType = genericName.TypeArgumentList.Arguments.First();

        var typeInformation = context.SemanticModel.GetTypeInfo(argumentType);

        return (INamedTypeSymbol)typeInformation.Type!;
    }
}