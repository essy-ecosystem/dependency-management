namespace DependencyManagement.Benchmarks.Versus.Startups;

using Labs;

public class Startup
{
    public static readonly DependencyManagementLab DependencyManagementLab = new();
    
    public static readonly MicrosoftDILab MicrosoftDependencyInjectionLab = new();
    
    public static readonly AutofacLab AutofacLab = new();
}