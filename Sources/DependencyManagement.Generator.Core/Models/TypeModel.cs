namespace DependencyManagement.Generator.Core.Models;

public sealed class TypeModel
{
    public TypeModel(string ns, string name)
    {
        Namespace = ns;
        Name = name;
    }

    public string Namespace { get; }

    public string Name { get; }

    public string FullName => $"{Namespace}.{Name}";

    public override string ToString()
    {
        return FullName;
    }
}