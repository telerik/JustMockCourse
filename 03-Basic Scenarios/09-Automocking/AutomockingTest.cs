using FluentAssertions;

namespace JustMockCourse.BasicScenarios.Automocking;

public class SmartHome
{
  private readonly ILights lights;
  private readonly ITelevision tv;
  private readonly ISpeakers speakers;

  public SmartHome(ILights lights, ITelevision tv, ISpeakers speakers)
  {
    this.lights = lights;
    this.tv = tv;
    this.speakers = speakers;
  }

  public void SetChannel(int channel)
  {
    tv.Channel = channel;
  }

  public int GetVolume() => speakers.Volume;
}

public class AutomockingTest
{
  [Fact]
  public void ShouldMockDependenciesWithoutContainer()
  {
    // Arrange 
    ILights lights = null;
    ITelevision tv = null;
    ISpeakers speakers = null;

    SmartHome home = new(lights, tv, speakers);

    int expectedChannel = 10;
    int expectedVolume = 75;

    // Act 
    int actualVolume = home.GetVolume(); 
    home.SetChannel(expectedChannel);


    // Assert
    actualVolume.Should().Be(expectedVolume);
    tv.Channel.Should().Be(expectedChannel);
  }

  [Fact]
  public void ShouldMockDependenciesWithContainer()
  {
    // Arrange 
    SmartHome home = null;

    int expectedChannel = 10;
    int expectedVolume = 75;

    

    // Act 
    int actualVolume = home.GetVolume();
    home.SetChannel(expectedChannel);


    // Assert
    actualVolume.Should().Be(expectedVolume);
    //container.AssertSet<ITelevision>(tv => tv.Channel = expectedChannel);
  }
}