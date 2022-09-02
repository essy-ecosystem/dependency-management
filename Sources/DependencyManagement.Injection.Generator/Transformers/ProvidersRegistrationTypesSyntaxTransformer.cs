namespace DependencyManagement.Injection.Generator.Transformers;

using DependencyManagement.Generator.Core.Models;
using DependencyManagement.Generator.Core.Transformers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;

public sealed class ProvidersRegistrationTypesSyntaxTransformer : ISyntaxTransformer<ProvidedTypeModel?>
{
    public ProvidedTypeModel? Transform(GeneratorSyntaxContext context)
    {
        var providedType = GetProvidedTypeFromGeneric(context);

        return ValidateProvidedType(providedType)
            ? GetProvidedTypeModel(providedType, GetRecommendedConstructor(providedType))
            : null;
    }

    private ProvidedTypeModel GetProvidedTypeModel(INamedTypeSymbol providedType, IMethodSymbol constructorMethod)
    {
        var ns = providedType.ContainingNamespace.ToDisplayString();
        var name = providedType.Name;

        var type = new TypeModel(ns, name);

        return new ProvidedTypeModel(type, GetConstructorArguments(constructorMethod));
    }

    private IReadOnlyList<TypeModel> GetConstructorArguments(IMethodSymbol constructor)
    {
        var parameters = constructor.Parameters;

        return parameters.Select(GetParameter).ToArray();
    }

    private TypeModel GetParameter(IParameterSymbol parameter)
    {
        var type = parameter.Type;

        var ns = type.ContainingNamespace.ToDisplayString();
        var name = type.Name;

        return new TypeModel(ns, name);
    }

    private IMethodSymbol GetRecommendedConstructor(INamedTypeSymbol providedType)
    {
        var providedTypeConstructors = providedType.InstanceConstructors;

        return providedTypeConstructors.OrderBy(method => method.Parameters.Length).First();
    }

    private bool ValidateProvidedType(INamedTypeSymbol providedType)
    {
        return !providedType.IsAbstract && providedType.TypeKind is TypeKind.Class;
    }

    private INamedTypeSymbol GetProvidedTypeFromGeneric(GeneratorSyntaxContext context)
    {
        var invocationExpression = (InvocationExpressionSyntax)context.Node;
        var memberAccessExpression = (MemberAccessExpressionSyntax)invocationExpression.Expression;

        var genericName = (GenericNameSyntax)memberAccessExpression.Name;
        var argumentType = genericName.TypeArgumentList.Arguments.First();

        var typeInformation = context.SemanticModel.GetTypeInfo(argumentType);

        return (INamedTypeSymbol)typeInformation.Type!;
    }
}