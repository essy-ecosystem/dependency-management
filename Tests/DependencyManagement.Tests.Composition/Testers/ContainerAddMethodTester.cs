namespace DependencyManagement.Tests.Composition.Testers;

using Containers;
using Fakes;

public static class ContainerAddMethodTester
{
    [Fact]
    public static void TestGetAddOfComponentsAddableEqualsOfNotSameTypeWhereItIsResult()
    {
        using var container = new Container();

        var component = new FakeComponent();
        
        container.Add(component);
        Assert.Equal(component, container.All<FakeComponent>().First());
        
        container.Add<IFakeComponent>(component);
        Assert.Equal(component, container.All<IFakeComponent>().First());
    }
}