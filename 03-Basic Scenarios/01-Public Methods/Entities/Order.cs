using JustMockCourse.BasicScenarios.PublicMethods;

public class Order
{
  public Order(string productName, int quantity)
  {
    this.ProductName = productName;
    this.Quantity = quantity;
  }

  public string ProductName { get; private set; }
  public int Quantity { get; private set; }
  public bool IsCompleted { get; private set; }

  public void Complete(Warehouse warehouse)
  {
    if (warehouse.HasInventory(this.ProductName, this.Quantity))
    {
      warehouse.Remove(this.ProductName, this.Quantity);
      IsCompleted = true;
    }
  }
}