using FluentAssertions;

namespace JustMockCourse.BasicScenarios.Generics;

public class Calculator
{
  public T Add<T>(T first, T second)
  {
    throw new NotImplementedException();
  }
}

public class Calculator<T>
{
  public T Add(T first, T second)
  {
    throw new NotImplementedException();
  }
}

public class GenericsTest
{
  [Fact]
  public void ShouldDistinguishCallsDependingOnArgumentTypes()
  {
    // Arrange 
    Calculator calculator = new();

    int expectedWithInts = 5;
    string expectedWithStrings = "seven";

    // Act 
    int actualWithInts = calculator.Add(2, 3);
    string actualWithStrings = calculator.Add("three", "four");

    // Assert 
    actualWithInts.Should().Be(expectedWithInts);
    actualWithStrings.Should().Be(expectedWithStrings);
  }

  [Fact]
  public void ShouldMockGenericClass()
  {
    // Arrange 
    Calculator<int> calculator = new();

    int expectedWithInts = 10;

    // Act 
    int actual = calculator.Add(4, 6);

    // Assert 
    actual.Should().Be(expectedWithInts);
  }
}