using FluentAssertions;
using Telerik.JustMock;
using Telerik.JustMock.Core;

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

    Mock.Arrange(() => collection[0]).Returns("dog");
    Mock.Arrange(() => collection[1]).Returns("cat");

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
    Animals collection = Mock.Create<Animals>(Behavior.Strict);

    Mock.ArrangeSet(() => { collection[0] = Arg.Matches<string>(x => x.Equals("dog")); });
    Mock.ArrangeSet(() => { collection[1] = Arg.IsAny<string>(); });

    // Act 
    Action firstAction = () =>
    {
      collection[0] = "dog";
      collection[1] = "cat";
    };

    Action secondAction = () => collection[5] = "zebra";

    // Assert 
    firstAction.Should().NotThrow();
    secondAction.Should().Throw<StrictMockException>();
  }
}