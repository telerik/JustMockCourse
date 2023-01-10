using FluentAssertions;

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
    IEntity entity = null;

    // Act 
    int actual = entity.Id;

    // Assert 
    actual.Should().Be(25);
  }

  [Fact]
  public void ShouldAssertPropertySet()
  {
    // Arrange 
    IEntity entity = null;

    // Act 
    entity.Id = 1;

    // Assert 
    // Mock.AssertSet(() => entity.Id = 1);
  }

  [Fact]
  public void ShouldThrowExceptionOnTheThirdPropertySetCall()
  {
    // Arrange 
    IEntity entity = null;

    // Act 
    Action firstAction = () => entity.Id = 5;
    Action secondAction = () => entity.Id = 15;

    // Assert 
    // firstAction.Should().NotThrow();
    // secondAction.Should().Throw<StrictMockException>();
  }
}