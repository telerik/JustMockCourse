using FluentAssertions;
using Telerik.JustMock;

namespace JustMockCourse.BasicScenarios.PublicMethods;

public class OrderTest
{
  [Fact]
  public void IsCompletedWhenWareHouseHasInventory()
  {
    // Arrange
    Order order = new Order("testProduct", 10);
    Warehouse warehouse = new Warehouse();
    
    Mock.Arrange(() => warehouse.HasInventory(order.ProductName, order.Quantity)).Returns(true);

    Mock.Arrange(() => warehouse.Remove(order.ProductName, order.Quantity)).DoNothing();

    // Act
    order.Complete(warehouse);

    // Assert
    order.IsCompleted.Should().BeTrue();
  }
}
