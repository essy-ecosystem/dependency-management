namespace DependencyManagement.Tests.Composition.Testers;

using Containers;
using Fakes;

public static class ContainerAllMethodTester
{
    [Fact]
    public static void TestGetAllOfEmptyContainsWhereEmptyIsResult()
    {
        using var container = new Container();

        Assert.Equal(0, container.All<IFakeComponent>().Count);
        Assert.Equal(0, container.All<IFakeComponent>().Count);
        Assert.Equal(0, container.All<FakeComponent>().Count);
        Assert.Equal(0, container.All<FakeComponent>().Count);
    }
    
    [Fact]
    public static void TestGetAllOfContainsComponentsOfNotSameTypesWhereTheyIsResult()
    {
        using var container = new Container();
        Assert.Equal(0, container.All<IFakeComponent>().Count);
        
        container.Add<IFakeComponent>(new FakeComponent());
        Assert.Equal(1, container.All<IFakeComponent>().Count);
        
        container.Add<IFakeComponent>(new FakeComponent());
        Assert.Equal(2, container.All<IFakeComponent>().Count);

        for (var i = 0; i < 100; i++) container.Add<IFakeComponent>(new FakeComponent());
        Assert.Equal(102, container.All<IFakeComponent>().Count);
    }
    
    [Fact]
    public static void TestGetAllOfInheritContainsComponentsOfNotSameTypesWhereTheyIsResult()
    {
        using var container = new Container();
        
        using var container1 = new Container(container);
        container1.Add<IFakeComponent>(new FakeComponent());

        using var container2 = new Container(container1);
        container2.Add<IFakeComponent>(new FakeComponent());
        
        using var container3 = new Container(container2);
        container3.Add<IFakeComponent>(new FakeComponent());
        
        Assert.Equal(0, container.All<IFakeComponent>(true).Count);
        Assert.Equal(1, container1.All<IFakeComponent>(true).Count);
        Assert.Equal(2, container2.All<IFakeComponent>(true).Count);
        Assert.Equal(3, container3.All<IFakeComponent>(true).Count);
        
        container1.Add<IFakeComponent>(new FakeComponent());
        container1.Add<IFakeComponent>(new FakeComponent());
        
        container2.Add<IFakeComponent>(new FakeComponent());
        container2.Add<IFakeComponent>(new FakeComponent());
        
        container3.Add<IFakeComponent>(new FakeComponent());
        container3.Add<IFakeComponent>(new FakeComponent());
        
        Assert.Equal(9, container3.All<IFakeComponent>(true).Count);
    }
}