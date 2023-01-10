using FluentAssertions;
using Telerik.JustMock;

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

    Mock.Arrange(() => calculator.Add(Arg.AnyInt, Arg.AnyInt)).Returns(expectedWithInts);
    Mock.Arrange(() => calculator.Add(Arg.AnyString, Arg.AnyString)).Returns(expectedWithStrings);

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
    Calculator<int> calculator = Mock.Create<Calculator<int>>();

    int expectedWithInts = 10;

    Mock.Arrange(() => calculator.Add(Arg.AnyInt, Arg.AnyInt)).Returns(expectedWithInts);

    // Act 
    int actual = calculator.Add(4, 6);

    // Assert 
    actual.Should().Be(expectedWithInts);
  }
}