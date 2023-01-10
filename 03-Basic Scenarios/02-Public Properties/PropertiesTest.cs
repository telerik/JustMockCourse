using FluentAssertions;
using Telerik.JustMock;
using Telerik.JustMock.Core;

namespace JustMockCourse.BasicScenarios.PublicProperties;

public interface IEntity
{
  int Id { get; set; }
}

public class PropertiesTest
{
  [Fact]
  public void ShouldFakePropertyGet()
  {
    // Arrange 
    IEntity entity = Mock.Create<IEntity>();

    Mock.Arrange(() => entity.Id).Returns(25);

    // Act 
    int actual = entity.Id;

    // Assert 
    actual.Should().Be(25);
  }

  [Fact]
  public void ShouldAssertPropertySet()
  {
    // Arrange 
    IEntity entity = Mock.Create<IEntity>();

    Mock.ArrangeSet(() => entity.Id = 1);

    // Act 
    entity.Id = 1;

    // Assert 
    Mock.AssertSet(() => entity.Id = 1);
  }

  [Fact]
  public void ShouldThrowExceptionOnTheThirdPropertySetCall()
  {
    // Arrange 
    IEntity entity = Mock.Create<IEntity>(Behavior.Strict);

    Mock.ArrangeSet(() => entity.Id = Arg.Matches<int>(x => x < 10));

    // Act 
    Action firstAction = () => entity.Id = 5;
    Action secondAction = () => entity.Id = 15;

    // Assert 
    firstAction.Should().NotThrow();
    secondAction.Should().Throw<StrictMockException>();
  }
}