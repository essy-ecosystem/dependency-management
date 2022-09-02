namespace DependencyManagement.Modularity.Generator.Builders;

using System.Diagnostics;
using System.Text;
using DependencyManagement.Generator.Core.Models;
using Injection.Generator.Generators;

internal class CompositeExtensionsBuilder
{
    private readonly string _assembly;

    private readonly IReadOnlyCollection<TypeModel> _extensionsMethods;

    private readonly IReadOnlyCollection<TypeModel> _modulesTypes;

    public CompositeExtensionsBuilder(string assembly, IReadOnlyCollection<TypeModel> modulesTypes,
        IReadOnlyCollection<TypeModel> extensionsMethods)
    {
        _assembly = assembly;
        _modulesTypes = modulesTypes;
        _extensionsMethods = extensionsMethods;
    }

    public override string ToString()
    {
        var assemblyDisplayName = _assembly.Replace(".", "");
        var methodDisplayName = $"WithModulesFrom{assemblyDisplayName}Assembly";

        var builder = new StringBuilder();
        builder.AppendLine("using DependencyManagement.Composition.Composites;");
        builder.AppendLine("using DependencyManagement.Composition.Utils;");
        builder.AppendLine("using DependencyManagement.Modularity.Extensions;");
        builder.AppendLine();
        builder.AppendLine("namespace DependencyManagement.Modularity.Extensions;");
        builder.AppendLine();
        builder.AppendLine(
            $"[System.CodeDom.Compiler.GeneratedCode(\"{nameof(ModulesGenerator)}\", \"{FileVersionInfo.GetVersionInfo(GetType().Assembly.Location).ProductVersion}\")]");
        builder.AppendLine($"public static class {assemblyDisplayName}AssemblyGeneratedCompositeExtensions");
        builder.AppendLine("{");
        builder.AppendLine("    internal static T WithModules<T>(this T composite) where T : class, IComposite");
        builder.AppendLine("    {");
        builder.AppendLine($"        composite.{methodDisplayName}();");
        foreach (var method in _extensionsMethods)
        {
            builder.AppendLine($"        composite.{method.Name}();");
        }

        builder.AppendLine("        return composite;");
        builder.AppendLine("    }");
        builder.AppendLine();
        builder.AppendLine($"    public static T {methodDisplayName}<T>(this T composite) where T : class, IComposite");
        builder.AppendLine("    {");
        builder.AppendLine("        var rootComposite = CompositeTreeUtils.GetLast(composite);");
        foreach (var module in _modulesTypes)
        {
            builder.AppendLine(
                $"        rootComposite.TrySetLazyModule(() => new {module}());");
        }

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