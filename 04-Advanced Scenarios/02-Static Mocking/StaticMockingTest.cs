using FluentAssertions;

namespace JustMockCourse.AdvancedScenarios.StaticMocking;

public class Calculator
{
  static Calculator() { }
  public static int Add(int a, int b) => throw new NotImplementedException();
}

public static class CalculatorExtension
{
  public static int Multiply(this Calculator calculator, int a, int b) => throw new NotImplementedException();
}

public class StaticMockingTest
{
  [Fact]
  public void ShouldMockStaticCall()
  {
    // ARRANGE

    // ACT
    int actual = Calculator.Add(1, 2);

    // ASSERT
    actual.Should().Be(10);
  }

  [Fact]
  public void ShouldMockExtensionMethod()
  {
    // ARRANGE 
    Calculator calc = new();

    // ACT
    int actual = calc.Multiply(2, 2);

    // ASSERT
    actual.Should().Be(20);
  }
}


