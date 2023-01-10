using FluentAssertions;

namespace JustMockCourse.AdvancedScenarios.FutureMocking;

public class UserData
{
  public int Age { get; set; }
  public List<string> Friends { get; set; }
}

public class FutureMockingTest
{
  [Fact]
  public void ShouldArrangeReturnOnlyForSpecificInstance()
  {
    // ARRANGE 
    UserData userData = null;

    // ASSERT
    userData.Age.Should().Be(25);
    new UserData().Age.Should().Be(0);
  }

  [Fact]
  public void ShouldArrangeReturnForFutureUserDataInstances()
  {
    // ARRANGE 
    UserData userData = null;

    // ASSERT
    userData.Age.Should().Be(21);
    new UserData().Age.Should().Be(21);
    new UserData().Age.Should().Be(21);
  }

  public List<string> FakeFriends => new() { "Joey", "Chandler", "Monica" };

  [Fact]
  public void ShouldReturnFakeCollectionForFutureCall()
  {
    // ARRANGE 
    UserData userData = null;
    var expectedFriends = FakeFriends;

    // ACT
    var actualArrangedCollection = userData.Friends;
    var actualUnarrangedCollection = new UserData().Friends;


    // ASSERT
    actualArrangedCollection.Should().Equal(expectedFriends);
    actualUnarrangedCollection.Should().Equal(expectedFriends);
  }
}
