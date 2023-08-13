using System.Net;
using System.Text;
using EppendorfAuthApi.Core;
using Xunit;

namespace EppendorfAuth.Tests;

public class FunctionTests : IClassFixture<Setup>
{
    private readonly User _existingTestUser = User.Create("test", "test");

    private readonly HttpClient _httpClient = new();

    [Fact]
    public async void ShouldSuccessfullyAuthorizeWithValidCredentials()
    {
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{Setup.ApiUrl}/authorize"),
            Headers = {
                { "authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_existingTestUser.Username}:{_existingTestUser.Password}"))}" },
            },
        };

        var httpResponse = await _httpClient.SendAsync(httpRequestMessage);

        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}