namespace DependencyManagement.Injection.Generator.Generators;

using DependencyManagement.Generator.Core.Extensions;
using DependencyManagement.Generator.Core.Transformers;
using Microsoft.CodeAnalysis;
using Modularity.Generator.Builders;
using Modularity.Generator.Discovers;
using Modularity.Generator.Transformers;

[Generator]
public sealed class ModulesGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var modulesTypes = context.SyntaxProvider.Transform(
                new ModulesRegistrationTypesSyntaxDiscover(),
                new ModulesRegistrationTypesSyntaxTransformer())
            .Where(modulesTypes => modulesTypes is not null)
            .Select((modulesTypes, _) => modulesTypes!)
            .Collect();

        var assembly = context.CompilationProvider.Transform(
            new AssemblyNameCompilationTransformer());

        var methods = context.CompilationProvider.Transform(
            new ModulesExtensionsMethodsTypesCompilationTransformer());

        var liner = assembly.Combine(methods).Combine(modulesTypes);

        context.RegisterSourceOutput(liner, static (context, liner) =>
        {
            var builder = new CompositeExtensionsBuilder(liner.Left.Left,
                liner.Right, liner.Left.Right);

            context.AddSource($"CompositeExtensions.{nameof(ModulesGenerator)}.cs", builder.ToString());
        });
    }
}