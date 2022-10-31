namespace DependencyManagement.Tests.Composition.Testers;

using Containers;
using Fakes;

public static class ContainerAnyMethodTester
{
    [Fact]
    public static void TestGetAnyOfComponentsInContainerWhereAnyResult()
    {
        using var container = new Container();
        Assert.False(container.Any<FakeComponent>());
        
        container.Add(new FakeComponent());
        Assert.True(container.Any<FakeComponent>());

        container.Add(new FakeComponent());
        Assert.True(container.Any<FakeComponent>());
        
        for (var i = 0; i < 100; i++) container.Add(new FakeComponent());
        Assert.True(container.Any<FakeComponent>());
    }
    
    [Fact]
    public static void TestGetContainsInheritOfComponentsInContainerWhereTheyContainsResult()
    {
        using var container = new Container();
        using var container1 = new Container(container);
        
        Assert.False(container.Any<FakeComponent>(true));
        Assert.False(container1.Any<FakeComponent>(true));
        
        container.Add(new FakeComponent());
        container1.Add(new FakeComponent());
        
        Assert.True(container.Any<FakeComponent>(true));
        Assert.True(container1.Any<FakeComponent>(true));
    }
}