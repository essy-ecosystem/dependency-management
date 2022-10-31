namespace DependencyManagement.Tests.Composition.Testers;

using Containers;
using Fakes;

public static class ContainerContainsMethodTester
{
    [Fact]
    public static void TestGetContainsOfComponentsInContainerWhereTheyContainsResult()
    {
        using var container = new Container();

        var component = new FakeComponent();
        Assert.False(container.Contains(component));
        
        container.Add(component);
        Assert.True(container.Contains(component));

        var component1 = new FakeComponent();
        Assert.False(container.Contains(component1));
        
        container.Add(component1);
        Assert.True(container.Contains(component1));
        
        var component2 = new FakeComponent();
        for (var i = 0; i < 100; i++) container.Add(new FakeComponent());
        container.Add(component2);
        Assert.True(container.Contains(component2));
    }
    
    [Fact]
    public static void TestGetAnyInheritOfComponentsInContainerWhereTheyAnyResult()
    {
        using var container = new Container();

        var component = new FakeComponent();
        container.Add(component);
        
        using var container1 = new Container(container);
        
        var component1 = new FakeComponent();
        container1.Add(component1);

        Assert.False(container1.Contains(component));
        Assert.False(container.Contains(component1));
        Assert.True(container1.Contains(component1, true));
        Assert.True(container1.Contains(component, true));
    }
}