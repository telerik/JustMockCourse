namespace JustMockCourse.GettingStarted;

public class State
{
  public string Name { get; }
  public string Abbreviation { get; }

  public State(string name, string abbreviation)
  {
    Name = name;
    Abbreviation = abbreviation;
  }
}