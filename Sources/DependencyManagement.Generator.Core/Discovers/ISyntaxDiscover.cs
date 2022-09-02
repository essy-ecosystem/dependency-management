namespace DependencyManagement.Generator.Core.Discovers;

using Microsoft.CodeAnalysis;

public interface ISyntaxDiscover
{
    bool Match(SyntaxNode node);
}