using EppendorfAuthApi.Core;

namespace EppendorfAuthApi.Implementations.Tests;

public class BasicAuthServiceTests
{
    private readonly Mock<ILoggingService> _loggingService;
    private readonly BasicAuthService _basicAuthService;

    public BasicAuthServiceTests()
    {
        _loggingService = new Mock<ILoggingService>();

        _basicAuthService = new BasicAuthService(_loggingService.Object);
    }

    [Fact]
    public void ShouldReturnTrueAndAssignBase64EncodedCredentials()
    {
        const string credentialsBase64Encoded = "dGVzdDp0ZXN0";

        var headers = new Dictionary<string, string> { { "authorization", "Basic dGVzdDp0ZXN0" } };

        _basicAuthService.AreCredentialsPresentAndValid(headers, out string credentials)
            .Should()
            .BeTrue();

        credentials.Should().Be(credentialsBase64Encoded);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("Bearer testjwt")]
    [InlineData("Basic non.base64.encoded.value")]
    public void ShouldReturnFalse(string? basicAuthCredentials)
    {
        var headers = new Dictionary<string, string> { };

        if (basicAuthCredentials is not null)
        {
            headers.Add("authorization", basicAuthCredentials);
        }

        _basicAuthService.AreCredentialsPresentAndValid(headers, out string credentials)
            .Should()
            .BeFalse();

        credentials.Should().Be(string.Empty);

        _loggingService.Verify(logger => logger.LogInfo(It.IsAny<string>()));
    }

    [Fact]
    public void ShouldCorrectlyVerifyPassword()
    {
        const string password = "test";

        var passwordSha256 = password.ToSha256();

        _basicAuthService.CheckPassword(User.Create("test", passwordSha256), password)
            .Should()
            .BeTrue();
    }
}