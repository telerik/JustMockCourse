namespace JustMockCourse.GettingStarted;

public class City
{
  public string Name { get; }
  public State State { get; }

  public City(string name, State state)
  {
    Name = name;
    State = state;
  }
}