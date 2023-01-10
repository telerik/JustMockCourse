using FluentAssertions;

namespace JustMockCourse.BasicScenarios.AsyncMethods;

public class Customer
{
  public string Name { get; set; } = "Luke Skywalker";
}

public class CustomerRepo
{
  public async Task<Customer> GetByIdAsync(int id)
  {
    await Task.Delay(1000);
    return new Customer();
  }
}

public class AsyncMethodsTest
{
  [Fact]
  public async Task ShouldMockAsyncMethod()
  {
    // Arrange 
    CustomerRepo repo = new();

    Customer expected = new Customer() { Name = "Jean-Luc Picard" };

    // Act 
    Customer actual = await repo.GetByIdAsync(10);

    // Assert
    actual.Should().Be(expected);
  }

  [Fact]
  public void ShouldMockAsyncMethodFailure()
  {
    // Arrange 
    CustomerRepo repo = new();

    // Act 
    Task<Customer> result = repo.GetByIdAsync(20);

    // Assert
    result.IsFaulted.Should().BeTrue();
    result.IsCompleted.Should().BeTrue();
    result.Exception!.InnerException.Should().BeOfType<ArgumentException>();
  }

  [Fact]
  public async Task ShouldMockAsyncMethodFailureRealWorld()
  {
    // Arrange 
    CustomerRepo repo = new();

    // Act 
    Func<Task<Customer>> act = async () => await repo.GetByIdAsync(30);

    // Assert
    await act.Should().ThrowAsync<ArgumentException>();
  }
}