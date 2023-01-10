using FluentAssertions;

namespace JustMockCourse.BasicScenarios.Assertions;

interface IStringConverter
{
  string ToUpperCase(string value);
  string ToLowerCase(string value);
}

public class AssertionsTest
{
  [Fact]
  public void ShouldTestCallOccursExactlyThreeTimes()
  {
    // Arrange 
    IStringConverter converter = null;

    // Act 
    converter.ToUpperCase("click");
    converter.ToUpperCase("click");
    converter.ToUpperCase("boom");


    // Assert
    // Mock.Assert(() => converter.ToUpperCase(Arg.AnyString), Occurs.Exactly(3));
  }

  [Fact]
  public void ShouldTestCallOccursExactlyTwice()
  {
    // Arrange 
    IStringConverter converter = null;

    // Act 
    var act = () =>
    {
      converter.ToUpperCase("click");
      converter.ToUpperCase("click");
      converter.ToUpperCase("boom");
    };

    // Assert
    //act.Should().Throw<AssertFailedException>();
    //var assert = () => { Mock.Assert(converter); };
    //assert.Should().Throw<AssertFailedException>();
  }

  [Fact]
  public void ShouldTestCallOrder()
  {
    // Arrange 
    IStringConverter converter = null;

    // Act
    converter.ToLowerCase("CLICK");
    converter.ToLowerCase("CLICK");
    converter.ToUpperCase("boom");

    // Assert
    //Mock.Assert(converter);
  }
}