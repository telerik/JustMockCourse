using FluentAssertions;

namespace JustMockCourse.BasicScenarios.PublicProperties;

class Animals
{
  // Declare an array to store the data elements.
  private string[] arr = new string[100];

  // Define the indexer to allow client code to use [] notation.
  public string this[int i]
  {
    get { return arr[i]; }
    set { arr[i] = value; }
  }
}

public class IndexersTest
{
  [Fact]
  public void ShouldFakeIndexerGet()
  {
    // Arrange 
    Animals collection = new();

    // Act 
    string actualFirst = collection[0];
    string actualSecond = collection[1];

    // Assert 
    actualFirst.Should().Be("dog");
    actualSecond.Should().Be("cat");
  }

  [Fact]
  public void ShouldAssertIndexedSetWithMatcher()
  {
    // Arrange 
    Animals collection = new();

    // Act 
    Action firstAction = () =>
    {
      collection[0] = "dog";
      collection[1] = "cat";
    };

    Action secondAction = () => collection[0] = "zebra";

    // Assert 
    // firstAction.Should().NotThrow();
    // secondAction.Should().Throw<StrictMockException>();
  }
}