using FluentAssertions;

namespace JustMockCourse.GettingStarted;

public class InvoiceTest
{
  [Fact]
  public void AddLineItem()
  {
    // Arrange
    const int quantity = 1;
    Product product = new(Guid.NewGuid());
    State state = new("West Dakota", "WD");
    City city = new("Centreville", state);
    Address address = new("123 Blake St.", city, "12345");
    Customer customer = new(Guid.NewGuid(), address);
    Invoice invoice = new(customer);
    
    // Act
    invoice.AddItemQuantity(product, quantity);

    // Assert
    List<LineItem> lineItems = invoice.LineItems.ToList();
    lineItems.Count.Should().Be(1);
    LineItem actual = lineItems[0];
    LineItem expected = new(invoice, product, quantity);
    actual.Should().BeEquivalentTo(expected);
  }
}
