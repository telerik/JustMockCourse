using FluentAssertions;
using Telerik.JustMock;

namespace JustMockCourse.BasicScenarios.RefOutParams;

public interface IParser
{
  void Parse(ref string input);
  bool TryParse(string input, out string output);
}

public class RefOutTest
{
  [Fact]
  public void ShouldMockRefParam()
  {
    // Arrange 
    IParser parser = Mock.Create<IParser>();

    string expected = "Garcia";

    Mock.Arrange(() => parser.Parse(ref expected)).DoNothing();

    // Act 
    string actual = "Cherry";
    parser.Parse(ref actual);

    // Assert
    actual.Should().Be(expected);
  }

  [Fact]
  public void ShouldMockOutParam()
  {
    // Arrange 
    IParser parser = Mock.Create<IParser>();

    string expected = "Monkey";

    Mock.Arrange(() => parser.TryParse("Chunky", out expected)).Returns(true);

    // Act 
    bool itWorked = parser.TryParse("Chunky", out string actual);

    // Assert 
    itWorked.Should().BeTrue();
    actual.Should().Be(expected);
  }
}