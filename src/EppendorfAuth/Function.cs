using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using EppendorfAuthApi.Core;
using EppendorfAuthApi.Implementations;
using Microsoft.Extensions.DependencyInjection;


namespace EppendorfAuth;

public class Function : BaseApiIntegratedHttpFunction
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public Function() : this(null, null)
    {
    }

    internal Function(IUserRepository? userRepository = null,
        IAuthService? authService = null)
    {
        Startup.ConfigureServices();

        _userRepository = userRepository ?? Startup.Services.GetRequiredService<IUserRepository>();
        _authService = authService ?? Startup.Services.GetRequiredService<IAuthService>();
    }

    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
    {

        if (apigProxyEvent.HttpMethod != "POST") return NotAllowed();

        if (!_authService.AreCredentialsPresentAndValid(apigProxyEvent.Headers, out var credentials)) return BadRequest();

        var userCredentials = BaseAuthCredentials.FromBase64EncodedCredentials(credentials);

        if (userCredentials is null) return BadRequest();

        var user = await _userRepository.GetUserAsync(userCredentials.Username);

        if (user is null) return BadRequest();

        return _authService.CheckPassword(user, userCredentials.Password)
            ? Ok()
            : BadRequest();
    }
}
