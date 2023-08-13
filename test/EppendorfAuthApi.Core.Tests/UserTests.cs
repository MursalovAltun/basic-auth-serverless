namespace EppendorfAuthApi.Core;

public class UserTests
{
    [Fact]
    public void ShouldCorrectlyCreateAUser()
    {
        const string username = "test";
        const string password = "test";

        User.Create(username, password)
            .Should()
            .BeEquivalentTo(new User(username, password));
    }
}