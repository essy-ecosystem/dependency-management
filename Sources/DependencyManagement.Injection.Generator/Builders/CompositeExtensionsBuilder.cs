namespace DependencyManagement.Injection.Generator.Builders;

using System.Diagnostics;
using System.Text;
using DependencyManagement.Generator.Core.Models;
using Generators;
using Models;

internal class CompositeExtensionsBuilder
{
    private readonly string _assembly;

    private readonly IReadOnlyCollection<TypeModel> _extensionsMethods;

    private readonly IReadOnlyCollection<ProvidedTypeModel> _providedTypes;

    public CompositeExtensionsBuilder(string assembly, IReadOnlyCollection<ProvidedTypeModel> providedTypes,
        IReadOnlyCollection<TypeModel> extensionsMethods)
    {
        _assembly = assembly;
        _providedTypes = providedTypes;
        _extensionsMethods = extensionsMethods;
    }

    public override string ToString()
    {
        var assemblyDisplayName = _assembly.Replace(".", "");
        var methodDisplayName = $"WithProvidersFrom{assemblyDisplayName}Assembly";

        var builder = new StringBuilder();
        builder.AppendLine("using DependencyManagement.Composition.Composites;");
        builder.AppendLine("using DependencyManagement.Composition.Extensions;");
        builder.AppendLine("using DependencyManagement.Composition.Utils;");
        builder.AppendLine("using DependencyManagement.Injection.Providers;");
        builder.AppendLine();
        builder.AppendLine("namespace DependencyManagement.Injection.Extensions;");
        builder.AppendLine();
        builder.AppendLine(
            $"[System.CodeDom.Compiler.GeneratedCode(\"{nameof(ProvidersGenerator)}\", \"{FileVersionInfo.GetVersionInfo(GetType().Assembly.Location).ProductVersion}\")]");
        builder.AppendLine($"public static class {assemblyDisplayName}AssemblyGeneratedCompositeExtensions");
        builder.AppendLine("{");
        builder.AppendLine("    internal static T WithProviders<T>(this T composite) where T : class, IComposite");
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
        foreach (var providedType in _providedTypes)
        {
            builder.AppendLine(
                $"        rootComposite.TrySetLazyProvider<{providedType}>(() => new {providedType.Type.Name}GeneratedProvider());");
        }

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