namespace DependencyManagement.Injection.Generator.Models;

using DependencyManagement.Generator.Core.Models;

public sealed class ProvidedTypeModel
{
    public ProvidedTypeModel(TypeModel type, IReadOnlyList<TypeModel> arguments)
    {
        Type = type;
        Arguments = arguments;
    }

    public TypeModel Type { get; }

    public IReadOnlyList<TypeModel> Arguments { get; }

    public override string ToString()
    {
        return Type.ToString();
    }
}