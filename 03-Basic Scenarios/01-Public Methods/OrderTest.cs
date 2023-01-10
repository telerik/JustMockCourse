using FluentAssertions;

namespace JustMockCourse.BasicScenarios.PublicMethods;

public class OrderTest
{
  [Fact]
  public void IsCompletedWhenWareHouseHasInventory()
  {
    // Arrange
    Order order = new Order("testProduct", 10);
    Warehouse warehouse = new Warehouse();

    // Act
    order.Complete(warehouse);

    // Assert
    order.IsCompleted.Should().BeTrue();
  }
}
