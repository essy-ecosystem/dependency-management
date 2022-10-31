namespace DependencyManagement.Tests.Composition.Testers;

using Components;
using Containers;
using Fakes;

public static class LazyComponentTester
{
    [Fact]
    public static void TestGetClearOfComponentsInContainerWhereTheyClearResult()
    {
        var lazy = new LazyComponent<FakeComponent>(() => new());

        var component = lazy.Value;
        
        Assert.Equal(component, lazy.Value);
    }
}