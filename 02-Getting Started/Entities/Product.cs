namespace JustMockCourse.GettingStarted;

public class Product
{
  public Guid Guid { get; }

  public Product(Guid guid)
  {
    Guid = guid;
  }
}