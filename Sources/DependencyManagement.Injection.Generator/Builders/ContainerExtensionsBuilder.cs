namespace DependencyManagement.Injection.Generator.Builders;

using System.Diagnostics;
using System.Text;
using DependencyManagement.Generator.Core.Models;
using Generators;
using Models;

internal class ContainerExtensionsBuilder
{
    private readonly string _assembly;

    private readonly IReadOnlyCollection<TypeModel> _extensionsMethods;

    private readonly IReadOnlyCollection<ProvidedTypeModel> _providedTypes;

    public ContainerExtensionsBuilder(string assembly, IReadOnlyCollection<ProvidedTypeModel> providedTypes,
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
        builder.AppendLine("using DependencyManagement.Composition.Containers;");
        builder.AppendLine("using DependencyManagement.Composition.Extensions;");
        builder.AppendLine("using DependencyManagement.Composition.Utils;");
        builder.AppendLine("using DependencyManagement.Injection.Providers;");
        builder.AppendLine();
        builder.AppendLine("namespace DependencyManagement.Injection.Extensions;");
        builder.AppendLine();
        builder.AppendLine(
            $"[System.CodeDom.Compiler.GeneratedCode(\"{nameof(ProvidersGenerator)}\", \"{FileVersionInfo.GetVersionInfo(GetType().Assembly.Location).ProductVersion}\")]");
        builder.AppendLine($"public static class {assemblyDisplayName}AssemblyGeneratedContainerExtensions");
        builder.AppendLine("{");
        builder.AppendLine("    internal static T WithProviders<T>(this T container) where T : class, IContainer");
        builder.AppendLine("    {");
        builder.AppendLine($"        container.{methodDisplayName}();");
        foreach (var method in _extensionsMethods)
        {
            builder.AppendLine($"        container.{method.Name}();");
        }

        builder.AppendLine("        return container;");
        builder.AppendLine("    }");
        builder.AppendLine();
        builder.AppendLine($"    public static T {methodDisplayName}<T>(this T container) where T : class, IContainer");
        builder.AppendLine("    {");
        builder.AppendLine("        var rootContainer = TraversalService.GetInitial(container);");
        foreach (var providedType in _providedTypes)
        {
            builder.AppendLine(
                $"        rootContainer.TrySetLazyProvider<{providedType}>(() => new {providedType.Type.TypeName}GeneratedProvider());");
        }

        builder.AppendLine("        return container;");
        builder.AppendLine("    }");
        builder.AppendLine();
        builder.AppendLine(
            $"    public static T {methodDisplayName}WithDependencies<T>(this T container) where T : class, IContainer");
        builder.AppendLine("    {");
        builder.AppendLine("        container.WithProviders();");
        builder.AppendLine("        return container;");
        builder.AppendLine("    }");
        builder.AppendLine("}");
        return builder.ToString();
    }
}