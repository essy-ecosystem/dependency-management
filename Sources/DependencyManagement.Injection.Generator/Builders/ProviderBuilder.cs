namespace DependencyManagement.Injection.Generator.Builders;

using System.Diagnostics;
using System.Text;
using Models;

public sealed class ProviderBuilder
{
    private readonly ProvidedTypeModel _providedType;

    public ProviderBuilder(ProvidedTypeModel providedType)
    {
        _providedType = providedType;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine("using DependencyManagement.Composition.Containers;");
        builder.AppendLine("using DependencyManagement.Injection.Providers;");
        builder.AppendLine("using DependencyManagement.Injection.Extensions;");
        builder.AppendLine("using DependencyManagement.Composition.Enums;");
        builder.AppendLine();
        builder.AppendLine("namespace DependencyManagement.Injection.Providers;");
        builder.AppendLine();
        builder.AppendLine(
            $"[System.CodeDom.Compiler.GeneratedCode(\"{nameof(ProviderBuilder)}\", \"{FileVersionInfo.GetVersionInfo(GetType().Assembly.Location).ProductVersion}\")]");
        builder.AppendLine(
            $"public sealed class {_providedType.Type.TypeName}GeneratedProvider : MethodProvider<{_providedType}>");
        builder.AppendLine("{");
        builder.AppendLine($"    protected override {_providedType} CreateInstanceCore(IReadOnlyContainer container)");
        builder.AppendLine("    {");
        if (_providedType.Arguments.Count == 0)
        {
            builder.AppendLine("        return new();");
        }
        else if (_providedType.Arguments.Count == 1)
        {
            var argument = _providedType.Arguments.First();
            builder.AppendLine(
                $"        return new(container.LastInstance<{argument}>(TraversalStrategy.Inherit));");
        }
        else
        {
            for (var i = 0; i < _providedType.Arguments.Count; i++)
            {
                var argument = _providedType.Arguments[i];
                if (i == 0)
                {
                    builder.AppendLine(
                        $"        return new(container.LastInstance<{argument}>(TraversalStrategy.Inherit),");
                }
                else if (i == _providedType.Arguments.Count - 1)
                {
                    builder.AppendLine(
                        $"            container.LastInstance<{argument}>(TraversalStrategy.Inherit));");
                }
                else
                {
                    builder.AppendLine(
                        $"            container.LastInstance<{argument}>(TraversalStrategy.Inherit),");
                }
            }
        }

        builder.AppendLine("    }");
        builder.AppendLine("}");

        return builder.ToString();
    }
}