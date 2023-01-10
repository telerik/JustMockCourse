using FluentAssertions;

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
    IParser parser = null;

    string expected = "Garcia";

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
    IParser parser = null;

    string expected = "Monkey";

    // Act 
    bool itWorked = parser.TryParse("Chunky", out string actual);

    // Assert 
    itWorked.Should().BeTrue();
    actual.Should().Be(expected);
  }
}