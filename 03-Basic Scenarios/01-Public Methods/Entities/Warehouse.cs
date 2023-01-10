namespace JustMockCourse.BasicScenarios.PublicMethods;

public class Warehouse
{
  public Dictionary<string, int> Products { get; set; } = new();

  public bool HasInventory(string productName, int quantity)
  {
    int quantityInWarehouse;
    if (Products.TryGetValue(productName, out quantityInWarehouse))
    {
      if (quantityInWarehouse >= quantity)
      {
        return true;
      }
    }

    return false;
  }

  public void Remove(string productName, int quantity)
  {
    if (HasInventory(productName, quantity))
    {
      Products[productName] -= quantity;
    }
  }
}
