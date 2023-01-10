namespace JustMockCourse.BasicScenarios.Automocking;

public interface ILights
{
  int Intensity { get; set; }
}

public interface ITelevision
{
  int Channel { get; set; }
}

public interface ISpeakers
{
  int Volume { get; set; }
}

public interface IThermostat
{
  int Temperature { get; set; }
}