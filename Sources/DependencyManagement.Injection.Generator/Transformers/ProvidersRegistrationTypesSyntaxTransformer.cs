namespace DependencyManagement.Injection.Generator.Transformers;

using System.Text;
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

        var type = new TypeModel(ns, name, 
            providedType.IsGenericType ? GetTypeName(providedType) : null);

        return new ProvidedTypeModel(type, GetConstructorArguments(constructorMethod));
    }

    private string GetTypeName(INamedTypeSymbol type)
    {
        var builder = new StringBuilder(type.Name);

        if (!type.IsGenericType) return builder.ToString();

        builder.Append("<");

        var argsLength = type.TypeArguments.Length;

        for (var i = 0; i < argsLength; i++)
        {
            var argument = type.TypeArguments[i];

            builder.Append(argument.ContainingNamespace.ToDisplayString());
            builder.Append(".");
            builder.Append(GetTypeName((INamedTypeSymbol)argument));

            if (i != argsLength - 1) builder.Append(", ");
        }
        
        builder.Append(">");

        return builder.ToString();
    }

    private IReadOnlyList<TypeModel> GetConstructorArguments(IMethodSymbol constructor)
    {
        var parameters = constructor.Parameters;

        return parameters.Select(GetParameter).ToArray();
    }

    private TypeModel GetParameter(IParameterSymbol parameter)
    {
        var type = (INamedTypeSymbol)parameter.Type;

        var ns = type.ContainingNamespace.ToDisplayString();
        var name = type.Name;

        return new TypeModel(ns, name, type.IsGenericType ? GetTypeName(type) : null);
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