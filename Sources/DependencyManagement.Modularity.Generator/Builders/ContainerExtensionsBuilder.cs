namespace DependencyManagement.Modularity.Generator.Builders;

using System.Diagnostics;
using System.Text;
using DependencyManagement.Generator.Core.Models;
using Injection.Generator.Generators;

internal class ContainerExtensionsBuilder
{
    private readonly string _assembly;

    private readonly IReadOnlyCollection<TypeModel> _extensionsMethods;

    private readonly IReadOnlyCollection<TypeModel> _modulesTypes;

    public ContainerExtensionsBuilder(string assembly, IReadOnlyCollection<TypeModel> modulesTypes,
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
        builder.AppendLine("using DependencyManagement.Composition.Containers;");
        builder.AppendLine("using DependencyManagement.Composition.Utils;");
        builder.AppendLine("using DependencyManagement.Modularity.Extensions;");
        builder.AppendLine();
        builder.AppendLine("namespace DependencyManagement.Modularity.Extensions;");
        builder.AppendLine();
        builder.AppendLine(
            $"[System.CodeDom.Compiler.GeneratedCode(\"{nameof(ModulesGenerator)}\", \"{FileVersionInfo.GetVersionInfo(GetType().Assembly.Location).ProductVersion}\")]");
        builder.AppendLine($"public static class {assemblyDisplayName}AssemblyGeneratedContainerExtensions");
        builder.AppendLine("{");
        builder.AppendLine("    internal static T WithModules<T>(this T container) where T : class, IContainer");
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
        builder.AppendLine("        var rootContainer = ContainerTreeUtils.GetLast(container);");
        foreach (var module in _modulesTypes)
        {
            builder.AppendLine(
                $"        rootContainer.TrySetLazyModule(() => new {module}());");
        }

        builder.AppendLine("        return container;");
        builder.AppendLine("    }");
        builder.AppendLine();
        builder.AppendLine(
            $"    public static T {methodDisplayName}WithDependencies<T>(this T container) where T : class, IContainer");
        builder.AppendLine("    {");
        builder.AppendLine("        container.WithModules();");
        builder.AppendLine("        return container;");
        builder.AppendLine("    }");
        builder.AppendLine("}");
        return builder.ToString();
    }
}