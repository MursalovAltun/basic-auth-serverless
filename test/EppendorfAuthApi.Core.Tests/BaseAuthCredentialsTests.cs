namespace EppendorfAuthApi.Core;

public class BaseAuthCredentialsTests
{
    [Fact]
    public void ShouldCreateFromBase64EncodedCredentials()
    {
        BaseAuthCredentials.FromBase64EncodedCredentials("dGVzdDp0ZXN0")
            .Should()
            .BeEquivalentTo(new BaseAuthCredentials("test", "test"));
    }

    [Theory]
    [InlineData("dGVzdDo=")]
    [InlineData("OnRlc3Q=")]
    public void ShouldReturnNullWhenUsernameOrPasswordIsEmpty(string base64Credentials)
    {
        BaseAuthCredentials.FromBase64EncodedCredentials(base64Credentials)
            .Should()
            .BeNull();
    }
}