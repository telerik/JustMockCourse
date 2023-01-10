using FluentAssertions;
using Telerik.JustMock;

namespace JustMockCourse.AdvancedScenarios.NonPublicMocking;

public class Logger
{
  static private DateTime Now => DateTime.Now;

  public void Log(string message)
  {
    LogWithDateTime(message);
  }

  public void Log()
  {
    LogWithDateTime();
  }

  private void LogWithDateTime(string message)
  {
    Console.WriteLine($"{Now}: {message}");
  }

  private void LogWithDateTime()
  {
    Console.WriteLine($"{Now}: No message");
  }
}

public class NonPublicMockingTest
{
  [Fact]
  public void ShouldInvokeNonPublicMember()
  {
    // ARRANGE
    Logger logger = new Logger();

    string expected = "Et tu, Brute?";

    string actual = "";

    Mock.NonPublic
      .Arrange(logger, "LogWithDateTime", Arg.Expr.AnyString)
      .DoInstead((string message) => actual = message);

    // ACT
    logger.Log(expected);

    // ASSERT
    actual.Should().Be(expected);
  }

  [Fact]
  public void ShouldInvokeNonPublicMemberWithOverloads()
  {
    // ARRANGE
    Logger logger = new Logger();

    bool called = false;

    Mock.NonPublic
      .Arrange(logger, "LogWithDateTime")
      .DoInstead(() => called = true);

    // ACT
    logger.Log();

    // ASSERT
    called.Should().BeTrue();
  }

  [Fact]
  public void ShouldMockPrivateStaticProperty()
  {
    // ARRANGE
    Logger logger = new Logger();

    //var expected = "2022-12-24 12:00:00 AM: Carpe diem";
    Mock.NonPublic.Arrange<DateTime>(typeof(Logger), "Now").Returns(new DateTime(2022, 12, 24));

    var actual = "";
    Mock.Arrange(() => Console.WriteLine(Arg.AnyString)).DoInstead((string message) => actual = message);

    // ACT
    logger.Log("Carpe diem");

    // ASSERT
    // actual.Should().Be(expected);
  }
}


