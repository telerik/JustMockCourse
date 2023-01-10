namespace JustMockCourse.GettingStarted;

public class Customer
{
  public Guid Guid { get; }
  public Address Address { get; }

  public Customer(Guid guid, Address address)
  {
    Guid = guid;
    Address = address;
  }
}