using FluentAssertions;
using Telerik.JustMock;
using Telerik.JustMock.AutoMock;
using Telerik.JustMock.Helpers;

namespace JustMockCourse.BasicScenarios.Automocking;

public class SmartHome
{
  private readonly ILights lights;
  private readonly ITelevision tv;
  private readonly ISpeakers speakers;

  public SmartHome(ILights lights, ITelevision tv, ISpeakers speakers, IThermostat thermostat)
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
    ILights lights = Mock.Create<ILights>();
    ITelevision tv = Mock.Create<ITelevision>();
    ISpeakers speakers = Mock.Create<ISpeakers>();
    IThermostat thermostat= Mock.Create<IThermostat>();

    SmartHome home = new(lights, tv, speakers, thermostat);

    int expectedChannel = 10;
    int expectedVolume = 75;

    speakers.Arrange(sp => sp.Volume).Returns(expectedVolume);

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
    var container = new MockingContainer<SmartHome>();
    SmartHome home = container.Instance;

    int expectedChannel = 10;
    int expectedVolume = 75;

    container.Arrange<ISpeakers>(speakers => speakers.Volume).Returns(expectedVolume);

    // Act 
    int actualVolume = home.GetVolume();
    home.SetChannel(expectedChannel);


    // Assert
    actualVolume.Should().Be(expectedVolume);
    container.AssertSet<ITelevision>(tv => tv.Channel = expectedChannel);
  }
}