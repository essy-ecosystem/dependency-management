using System.Diagnostics;
using System.Text;
using DependencyManagement.Injection.Generator.Generators;

namespace DependencyManagement.Injection.Generator.Builders;

internal class CompositeExtensionsBuilder
{
    public CompositeExtensionsBuilder(string assembly, (string Namespace, string Type)[] providers, string[] methods)
    {
        Assembly = assembly;
        Providers = providers;
        Methods = methods;
    }

    public string Assembly { get; }

    public string[] Methods { get; }

    public (string Namespace, string Type)[] Providers { get; }

    public override string ToString()
    {
        var assemblyDisplayName = Assembly.Replace(".", "");
        var methodDisplayName = $"WithProvidersFrom{assemblyDisplayName}Assembly";

        var builder = new StringBuilder();
        builder.AppendLine("using DependencyManagement.Composition.Composites;");
        builder.AppendLine("using DependencyManagement.Composition.Extensions;");
        builder.AppendLine("using DependencyManagement.Composition.Utils;");
        builder.AppendLine("using DependencyManagement.Injection.Providers;");
        builder.AppendLine();
        builder.AppendLine("namespace DependencyManagement.Injection.Extensions;");
        builder.AppendLine();
        builder.AppendLine($"[System.CodeDom.Compiler.GeneratedCode(\"{nameof(ProvidersGenerator)}\", \"{FileVersionInfo.GetVersionInfo(GetType().Assembly.Location).ProductVersion}\")]");
        builder.AppendLine($"public static class {assemblyDisplayName}AssemblyGeneratedCompositeExtensions");
        builder.AppendLine("{");
        builder.AppendLine("    internal static T WithProviders<T>(this T composite) where T : class, IComposite");
        builder.AppendLine("    {");
        builder.AppendLine($"        composite.{methodDisplayName}();");
        foreach (var method in Methods) builder.AppendLine($"        composite.{method}();");
        builder.AppendLine("        return composite;");
        builder.AppendLine("    }");
        builder.AppendLine();
        builder.AppendLine($"    public static T {methodDisplayName}<T>(this T composite) where T : class, IComposite");
        builder.AppendLine("    {");
        builder.AppendLine("        var rootComposite = CompositeTreeUtils.GetLast(composite);");
        foreach (var provider in Providers)
            builder.AppendLine(
                $"        rootComposite.TrySetLazyProvider<{provider.Namespace}.{provider.Type}>(() => new {provider.Type}GeneratedProvider());");
        builder.AppendLine("        return composite;");
        builder.AppendLine("    }");
        builder.AppendLine();
        builder.AppendLine(
            $"    public static T {methodDisplayName}WithDependencies<T>(this T composite) where T : class, IComposite");
        builder.AppendLine("    {");
        builder.AppendLine("        composite.WithProviders();");
        builder.AppendLine("        return composite;");
        builder.AppendLine("    }");
        builder.AppendLine("}");
        return builder.ToString();
    }
}