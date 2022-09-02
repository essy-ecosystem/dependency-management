namespace DependencyManagement.Modularity.Generator.Discovers;

using DependencyManagement.Generator.Core.Discovers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public sealed class ModulesRegistrationTypesSyntaxDiscover : ISyntaxDiscover
{
    private const string AddMethod = "AddModule";

    public bool Match(SyntaxNode node)
    {
        return node is InvocationExpressionSyntax
        {
            Expression: MemberAccessExpressionSyntax
            {
                Name: GenericNameSyntax
                {
                    Identifier.Text: AddMethod
                } name
            }
        } && name.TypeArgumentList.Arguments.Count == 1;
    }
}