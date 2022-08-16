using System.Diagnostics;
using System.Text;
using DependencyManagement.Injection.Generator.Generators;

namespace DependencyManagement.Modularity.Generator.Builders;

internal class CompositeExtensionsBuilder
{
    public CompositeExtensionsBuilder(string assembly, (string Namespace, string Type)[] modules, string[] methods)
    {
        Assembly = assembly;
        Modules = modules;
        Methods = methods;
    }

    public string Assembly { get; }

    public string[] Methods { get; }

    public (string Namespace, string Type)[] Modules { get; }

    public override string ToString()
    {
        var assemblyDisplayName = Assembly.Replace(".", "");
        var methodDisplayName = $"WithModulesFrom{assemblyDisplayName}Assembly";

        var builder = new StringBuilder();
        builder.AppendLine("using DependencyManagement.Composition.Composites;");
        builder.AppendLine("using DependencyManagement.Composition.Utils;");
        builder.AppendLine("using DependencyManagement.Modularity.Extensions;");
        builder.AppendLine();
        builder.AppendLine("namespace DependencyManagement.Modularity.Extensions;");
        builder.AppendLine();
        builder.AppendLine($"[System.CodeDom.Compiler.GeneratedCode(\"{nameof(ModulesGenerator)}\", \"{FileVersionInfo.GetVersionInfo(GetType().Assembly.Location).ProductVersion}\")]");
        builder.AppendLine($"public static class {assemblyDisplayName}AssemblyGeneratedCompositeExtensions");
        builder.AppendLine("{");
        builder.AppendLine("    internal static T WithModules<T>(this T composite) where T : class, IComposite");
        builder.AppendLine("    {");
        builder.AppendLine($"        composite.{methodDisplayName}();");
        foreach (var method in Methods) builder.AppendLine($"        composite.{method}();");
        builder.AppendLine("        return composite;");
        builder.AppendLine("    }");
        builder.AppendLine();
        builder.AppendLine($"    public static T {methodDisplayName}<T>(this T composite) where T : class, IComposite");
        builder.AppendLine("    {");
        builder.AppendLine("        var rootComposite = CompositeTreeUtils.GetLast(composite);");
        foreach (var module in Modules)
            builder.AppendLine(
                $"        rootComposite.TrySetLazyModule(() => new {module.Namespace}.{module.Type}());");
        builder.AppendLine("        return composite;");
        builder.AppendLine("    }");
        builder.AppendLine();
        builder.AppendLine(
            $"    public static T {methodDisplayName}WithDependencies<T>(this T composite) where T : class, IComposite");
        builder.AppendLine("    {");
        builder.AppendLine("        composite.WithModules();");
        builder.AppendLine("        return composite;");
        builder.AppendLine("    }");
        builder.AppendLine("}");
        return builder.ToString();
    }
}