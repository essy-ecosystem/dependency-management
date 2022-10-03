namespace DependencyManagement.Generator.Core.Models;

public sealed class TypeModel
{
    public TypeModel(string ns, string name, string? generic = null)
    {
        Namespace = ns;
        TypeName = name;
        GenericName = generic;
    }

    public string Namespace { get; }

    public string Name => GenericName ?? TypeName;
    
    public string TypeName { get; }

    public string? GenericName { get; }

    public string FullName => $"{Namespace}.{Name}";

    public override string ToString()
    {
        return FullName;
    }
}