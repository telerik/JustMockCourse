namespace JustMockCourse.GettingStarted;

public class Invoice
{
  public Customer Customer { get; }
  private readonly List<LineItem> lineItems = new();
  public IEnumerable<LineItem> LineItems => lineItems;


  public Invoice(Customer customer)
  {
    Customer = customer;
  }

  public void AddItemQuantity(Product product, int quantity)
  {
    lineItems.Add(new LineItem(this, product, quantity));
  }
}