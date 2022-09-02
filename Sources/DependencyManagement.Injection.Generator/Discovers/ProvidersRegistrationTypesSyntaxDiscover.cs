namespace DependencyManagement.Injection.Generator.Discovers;

using DependencyManagement.Generator.Core.Discovers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public sealed class ProvidersRegistrationTypesSyntaxDiscover : ISyntaxDiscover
{
    private const string AddMethod = "AddTarget";

    private const string SetMethod = "SetTarget";

    private const string TrySetMethod = "TrySetTarget";

    public bool Match(SyntaxNode node)
    {
        return node is InvocationExpressionSyntax
        {
            Expression: MemberAccessExpressionSyntax
            {
                Name: GenericNameSyntax
                {
                    Identifier.Text: AddMethod or SetMethod or TrySetMethod
                } name
            }
        } && name.TypeArgumentList.Arguments.Count == 1;
    }
}