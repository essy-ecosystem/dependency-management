using System.Diagnostics;
using System.Text;

namespace DependencyManagement.Injection.Generator.Builders;

public sealed class ProviderBuilder
{
    public ProviderBuilder(string ns, string type, (string Namespace, string Type)[] arguments)
    {
        Namespace = ns;
        Type = type;
        Arguments = arguments;
    }

    public string Namespace { get; }

    public string Type { get; }

    public (string Namespace, string Type)[] Arguments { get; }

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine("using DependencyManagement.Composition.Composites;");
        builder.AppendLine("using DependencyManagement.Injection.Providers;");
        builder.AppendLine("using DependencyManagement.Injection.Extensions;");
        builder.AppendLine("using DependencyManagement.Composition.Enums;");
        builder.AppendLine();
        builder.AppendLine("namespace DependencyManagement.Injection.Providers;");
        builder.AppendLine();
        builder.AppendLine($"[System.CodeDom.Compiler.GeneratedCode(\"{nameof(ProviderBuilder)}\", \"{FileVersionInfo.GetVersionInfo(GetType().Assembly.Location).ProductVersion}\")]");
        builder.AppendLine($"internal sealed class {Type}GeneratedProvider : MethodProvider<{Namespace}.{Type}>");
        builder.AppendLine("{");
        builder.AppendLine($"    protected override {Namespace}.{Type} GetInstanceCore(IReadOnlyComposite composite)");
        builder.AppendLine("    {");
        if (Arguments.Length == 0)
        {
            builder.AppendLine("        return new();");
        }
        else if (Arguments.Length == 1)
        {
            var argument = Arguments.First();
            builder.AppendLine(
                $"        return new(composite.LastInstance<{argument.Namespace}.{argument.Type}>(CompositeTraversalStrategy.Inherit));");
        }
        else
        {
            for (var i = 0; i < Arguments.Length; i++)
            {
                var argument = Arguments[i];
                if (i == 0)
                    builder.AppendLine(
                        $"        return new(composite.LastInstance<{argument.Namespace}.{argument.Type}>(CompositeTraversalStrategy.Inherit),");
                else if (i == Arguments.Length - 1)
                    builder.AppendLine(
                        $"            composite.LastInstance<{argument.Namespace}.{argument.Type}>(CompositeTraversalStrategy.Inherit));");
                else
                    builder.AppendLine(
                        $"            composite.LastInstance<{argument.Namespace}.{argument.Type}>(CompositeTraversalStrategy.Inherit),");
            }
        }

        builder.AppendLine("    }");
        builder.AppendLine("}");

        return builder.ToString();
    }
}