namespace JustMockCourse.GettingStarted;

public class LineItem
{
  public Invoice Invoice { get; }
  public Product Product { get; }
  public int Quantity { get; }

  public LineItem(Invoice invoice, Product product, int quantity)
  {
    Invoice = invoice;
    Product = product;
    Quantity = quantity;
  }
}