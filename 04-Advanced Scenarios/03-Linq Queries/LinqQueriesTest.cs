using FluentAssertions;
using System.Collections;
using Telerik.JustMock;

namespace JustMockCourse.AdvancedScenarios.LinqQueries;

public class SuperSimpleData
{
  public IEnumerable<Product>? Products { get; }
}

public class SimpleData
{
  public ExtendedQuery<Product>? Products { get; }

  public ExtendedQuery<Category>? Categories { get; }

  public Product? GetProduct(int id)
  {
    return Products?.Where(x => x.ProductID == id).FirstOrDefault();
  }
}

public abstract class ExtendedQuery<T> : IEnumerable<T>
{
  public IEnumerator<T> GetEnumerator()
  {
    throw new NotImplementedException();
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    throw new NotImplementedException();
  }
}

public class Product
{
  public int ProductID { get; set; }

  public int CategoryID { get; set; }

  public string? ProductName { get; set; }

  public int UnitsInStock { get; set; }

  public string? QuantityPerUnit { get; set; }

  public bool Discontinued { get; set; }

  public int voa_class { get; set; }

  public int GetId()
  {
    return ProductID;
  }
}

public class Category
{
  public int CategoryID { get; set; }

  public string? CategoryName { get; set; }
}

public class LinqQueriesTest
{
  // This method will generate our expected products collection. 
  // We will use it in some of the following test methods.
  private List<Product> GetProducts() => new()
  {
    new Product()
    {
        ProductID = 1,
        CategoryID = 1,
        ProductName = "test product",
        UnitsInStock = 3,
        QuantityPerUnit = "1",
        Discontinued = false,
        voa_class = 1
    },
    new Product()
    {
        ProductID = 2,
        CategoryID = 1,
        ProductName = "Foo stuff",
        UnitsInStock = 50,
        QuantityPerUnit = "1",
        Discontinued = true,
        voa_class = 1
    },
    new Product()
    {
        ProductID = 3,
        CategoryID = 2,
        ProductName = "More Stuff",
        UnitsInStock = 0,
        QuantityPerUnit = "1",
        Discontinued = true,
        voa_class = 1
    }
  };

  // This method will generate our expected categories collection. 
  // We will use it in some of the following test methods.
  private List<Category> GetCategories() => new()
  {
    new Category() { CategoryID = 1, CategoryName = "First" },
    new Category() { CategoryID = 2, CategoryName = "Second" }
  };

  [Fact]
  public void ShouldAssertWithSelect()
  {
    // ARRANGE 
    var simpleData = new SuperSimpleData();
    Mock.Arrange(() => simpleData.Products).Returns(GetProducts());

    // ACT 
    var actual = simpleData.Products?
      .Where(p => p.UnitsInStock == 50)
      .Select(p => p.ProductID)
      .SingleOrDefault();

    // ASSERT
    actual.Should().Be(2);
  }


  [Fact]
  public void ShouldAssertWithCustomSelect()
  {
    // ARRANGE 
    var simpleData = new SimpleData();
    Mock.Arrange(() => simpleData.Products).ReturnsCollection(GetProducts());

    // ACT 
    var actual = simpleData.Products?
      .Where(p => p.UnitsInStock == 50)
      .Select(p => p.ProductID)
      .SingleOrDefault();

    // ASSERT
    actual.Should().Be(2);
  }

  [Fact]
  public void ShouldAssertWithJoinClause()
  {
    // ARRANGE
    var simpleData = new SimpleData();
    Mock.Arrange(() => simpleData.Products).ReturnsCollection(GetProducts());
    Mock.Arrange(() => simpleData.Categories).ReturnsCollection(GetCategories());

    // ACT 
    var actual = simpleData.Products?
      .Join(simpleData.Categories!, p => p.CategoryID, c => c.CategoryID, (p, c) => c.CategoryName);

    // ASSERT
    actual?.Count().Should().Be(3);
  }

  [Fact]
  public void ShouldAssertWithExpressionAsAnArgument()
  {
    // ARRANGE
    var simpleData = new SimpleData();
    int targetProductId = 2;
    int expectedId = 10;

    Mock.Arrange(() => simpleData.Products).ReturnsCollection(GetProducts());
    Mock.Arrange(() => simpleData.Products!
      .Where(x => x.ProductID == targetProductId)
      .First()
      .GetId())
      .Returns(expectedId)
      .MustBeCalled();


    // ACT
    Product actual = simpleData.GetProduct(targetProductId)!;

    // ASSERT
    actual.GetId().Should().Be(expectedId);
  }
}
