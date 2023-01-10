namespace JustMockCourse.GettingStarted;

public class Address
{
  public string StreetAddress { get; }
  public City City { get; }
  public string PostalCode { get; }

  public Address(string address, City city, string postalCode)
  {
    StreetAddress = address;
    City = city;
    PostalCode = postalCode;
  }
}
