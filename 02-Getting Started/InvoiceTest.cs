using FluentAssertions;
using Telerik.JustMock;

namespace JustMockCourse.GettingStarted;

public class InvoiceTest
{
  [Fact]
  public void AddLineItem()
  {
    // Arrange
    const int quantity = 1;
    Product product = new(Guid.NewGuid());
    Invoice invoice = new(Mock.Create<Customer>());
    
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
