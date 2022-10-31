namespace DependencyManagement.Tests.Composition.Testers;

using Containers;
using Fakes;

public static class ContainerRemoveMethodTester
{
    [Fact]
    public static void TestGetRemoveOfComponentsInContainerWhereTheyRemovesResult()
    {
        using var container = new Container();

        var component = new FakeComponent();
        Assert.False(container.Remove(component));
        
        container.Add(component);
        Assert.True(container.Remove(component));
        Assert.False(container.Contains(component));

        var component1 = new FakeComponent();
        container.Add(component);
        container.Add(component1);
        Assert.True(container.Remove(component));
        Assert.False(container.Contains(component));
        Assert.True(container.Remove(component1));
        Assert.False(container.Contains(component1));
        
        container.Add(component1);
        container.Add(component);
        Assert.True(container.Remove(component1));
        Assert.False(container.Contains(component1));
        Assert.True(container.Remove(component));
        Assert.False(container.Contains(component));
        
        var component2 = new FakeComponent();
        for (var i = 0; i < 100; i++) container.Add(new FakeComponent());
        container.Add(component2);
        Assert.True(container.Remove(component2));
        Assert.False(container.Contains(component2));
    }
    
    [Fact]
    public static void TestGetRemoveInheritOfComponentsInContainerWhereTheyRemoveResult()
    {
        using var container = new Container();
        using var container1 = new Container(container);
        
        var component = new FakeComponent();
        var component1 = new FakeComponent();
        
        Assert.False(container1.Remove(component, true));
        Assert.False(container.Remove(component1, true));
        
        container.Add(component);
        container.Add(new FakeComponent());
        container1.Add(new FakeComponent());
        container1.Add(component1);

        Assert.True(container1.Remove(component1, true));
        Assert.False(container1.Contains(component1, true));
        Assert.True(container1.Remove(component, true));
        Assert.False(container1.Contains(component, true));
    }
}