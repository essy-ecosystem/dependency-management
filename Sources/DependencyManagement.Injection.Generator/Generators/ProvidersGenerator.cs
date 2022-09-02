namespace DependencyManagement.Injection.Generator.Generators;

using Builders;
using DependencyManagement.Generator.Core.Extensions;
using DependencyManagement.Generator.Core.Transformers;
using Discovers;
using Microsoft.CodeAnalysis;
using Transformers;

[Generator]
public sealed class ProvidersGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var providedTypes = context.SyntaxProvider.Transform(
                new ProvidersRegistrationTypesSyntaxDiscover(),
                new ProvidersRegistrationTypesSyntaxTransformer())
            .Where(providedType => providedType is not null)
            .Select((providedType, _) => providedType!)
            .Collect();

        context.RegisterSourceOutput(providedTypes, static (context, providedTypes) =>
        {
            Parallel.ForEach(providedTypes, providedType =>
            {
                var builder = new ProviderBuilder(providedType);

                context.AddSource($"{providedType.Type.Name}GeneratedProvider.{nameof(ProvidersGenerator)}.cs",
                    builder.ToString());
            });
        });

        var assembly = context.CompilationProvider.Transform(
            new AssemblyNameCompilationTransformer());

        var methods = context.CompilationProvider.Transform(
            new ProvidersExtensionsMethodsTypesCompilationTransformer());

        var liner = assembly.Combine(methods).Combine(providedTypes);

        context.RegisterSourceOutput(liner, static (context, liner) =>
        {
            var builder = new CompositeExtensionsBuilder(liner.Left.Left,
                liner.Right, liner.Left.Right);

            context.AddSource($"CompositeExtensions.{nameof(ProvidersGenerator)}.cs", builder.ToString());
        });
    }
}