using FluentAssertions;
using Telerik.JustMock;
using Telerik.JustMock.XUnit;

namespace JustMockCourse.BasicScenarios.Expectations;

public enum Operation
{
  Add, Subtract, Multiply, Divide
}

public delegate void Notify(Operation operation);

public class Calculator
{
  public Operation Operation { get; set; } = Operation.Add;
  public int Execute(int first, int second)
  {
    int result = Operation switch
    {
      Operation.Add => first + second,
      Operation.Subtract => first - second,
      Operation.Multiply => first * second,
      Operation.Divide => first / second,
      _ => throw new NotImplementedException(),
    };

    OperationCompleted?.Invoke(Operation);
    return result;
  }

  public event Notify? OperationCompleted;
}

public class ExpectationsTest
{
  [Fact]
  public void ShouldTestReturns()
  {
    // Arrange 
    Calculator calculator = Mock.Create<Calculator>();

    Mock.Arrange(() => calculator.Operation).Returns(Operation.Subtract);
    Mock.Arrange(() => calculator.Execute(Arg.AnyInt, Arg.AnyInt)).Returns((int x, int y) => x * y);

    // Act 
    Operation operation = calculator.Operation;
    int execute = calculator.Execute(5, 10);


    // Assert
    operation.Should().Be(Operation.Subtract);
    execute.Should().Be(50);
  }

  [Fact]
  public void ShouldTestThrows()
  {
    // Arrange 
    Calculator calculator = Mock.Create<Calculator>();

    Mock.Arrange(() => calculator.Execute(Arg.AnyInt, Arg.AnyInt)).Throws<ArgumentException>();

    // Act 
    var act = () => calculator.Execute(1, 2);


    // Assert
    act.Should().Throw<ArgumentException>();
  }

  [Fact]
  public void ShouldTestCallOriginal()
  {
    // Arrange 
    Calculator calculator = Mock.Create<Calculator>();

    Mock.Arrange(() => calculator.Execute(1, 2)).CallOriginal();
    Mock.Arrange(() => calculator.Execute(2, 3)).Returns(10);

    // Act 
    int original = calculator.Execute(1, 2);
    int mocked = calculator.Execute(2, 3);

    // Assert
    original.Should().Be(3);
    mocked.Should().Be(10);
  }

  [Fact]
  public void ShouldTestMustBeCalled()
  {
    // Arrange 
    Calculator calculator = Mock.Create<Calculator>();
    Calculator calculator2 = Mock.Create<Calculator>();

    Mock.Arrange(() => calculator.Execute(Arg.AnyInt, Arg.AnyInt)).Returns(3).MustBeCalled();
    Mock.Arrange(() => calculator2.Execute(Arg.AnyInt, Arg.AnyInt)).Returns(6).MustBeCalled();

    // Act 
    calculator.Execute(5, 6);

    // Assert
    var assert = () => Mock.Assert(calculator);
    var assert2 = () => Mock.Assert(calculator2);
    assert.Should().NotThrow();
    assert2.Should().Throw<AssertFailedException>();
  }

  [Fact]
  public void ShouldTestDoNothing()
  {
    // Arrange 
    Calculator calculator = new Calculator();

    Mock.Arrange(() => calculator.Execute(Arg.AnyInt, Arg.AnyInt)).DoNothing().MustBeCalled();

    // Act 
    var actual = calculator.Execute(4, 5);


    // Assert
    Mock.Assert(calculator);
    actual.Should().Be(0);
  }

  [Fact]
  public void ShouldTestDoInstead()
  {
    // Arrange 
    Calculator calculator = Mock.Create<Calculator>();

    bool called = false;

    Mock.Arrange(() => calculator
      .Execute(Arg.AnyInt, Arg.AnyInt))
      .DoInstead(() => { called = true; })
      .Returns(20);

    // Act 
    var actual = calculator.Execute(7, 8);


    // Assert
    actual.Should().Be(20);
    called.Should().BeTrue();
  }

  [Fact]
  public void ShouldTestRaise()
  {
    // Arrange 
    Calculator calculator = Mock.Create<Calculator>();

    Operation expected = Operation.Multiply;
    Operation actual = Operation.Add;
    void handler(Operation o) { actual = o; };

    // Act
    Mock.Raise(() => calculator.OperationCompleted += handler, expected);

    // Assert
    actual.Should().Be(expected);
  }

  [Fact]
  public void ShouldTestRaises()
  {
    // Arrange 
    Calculator calculator = Mock.Create<Calculator>();

    Operation expected = Operation.Divide;
    Operation actual = Operation.Subtract;
    void handler(Operation o) { actual = o; };

    Mock.Arrange(() => calculator.Execute(Arg.AnyInt, Arg.AnyInt)).Raises(() => calculator.OperationCompleted += handler, expected);

    // Act 
    calculator.Execute(9, 10);


    // Assert
    actual.Should().Be(expected);
  }

  [Fact]
  public void ShouldTestWhen()
  {
    // Arrange 
    Calculator calculator = Mock.Create<Calculator>();

    Mock.Arrange(() => calculator.Execute(Arg.AnyInt, Arg.AnyInt)).When(() => calculator.Operation == Operation.Divide).Returns(100);

    // Act 
    calculator.Operation = Operation.Divide;
    int whenTrue = calculator.Execute(10, 11);
    calculator.Operation = Operation.Subtract;
    int whenFalse = calculator.Execute(11, 12);

    // Assert
    whenTrue.Should().Be(100);
    whenFalse.Should().Be(0);
  }
}