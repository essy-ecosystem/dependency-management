namespace DependencyManagement.Tests.Composition.Testers;

using Containers;
using Fakes;

public static class ContainerClearMethodTester
{
    [Fact]
    public static void TestGetClearOfComponentsInContainerWhereTheyClearResult()
    {
        using var container = new Container();

        for (var i = 0; i < 10; i++) container.Add(new FakeComponent());
        
        Assert.Equal(10, container.All<FakeComponent>().Count);
        
        container.Clear<FakeComponent>();
        
        Assert.Equal(0, container.All<FakeComponent>().Count);
    }
    
    [Fact]
    public static void TestGetClearInheritOfComponentsInContainerWhereTheyClearResult()
    {
        using var container = new Container();
        using var container1 = new Container(container);

        for (var i = 0; i < 10; i++)
        {
            container.Add(new FakeComponent());
            container1.Add(new FakeComponent());
        }
        
        Assert.Equal(20, container1.All<FakeComponent>(true).Count);
        
        container1.Clear<FakeComponent>(true);
        
        Assert.Equal(0, container1.All<FakeComponent>(true).Count);
    }
}