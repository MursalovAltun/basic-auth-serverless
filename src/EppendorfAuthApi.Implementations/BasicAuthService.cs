using System.Text.RegularExpressions;
using EppendorfAuthApi.Core;

namespace EppendorfAuthApi.Implementations;

public class BasicAuthService : IAuthService
{
    private const string AuthorizationHeaderName = "authorization";
    private readonly Regex BasicAuthTypeRegex = new(@"^(basic )", RegexOptions.IgnoreCase);

    private readonly ILoggingService _loggingService;

    public BasicAuthService(ILoggingService loggingService)
    {
        _loggingService = loggingService;
    }

    public bool AreCredentialsPresentAndValid(IDictionary<string, string> headers, out string credentials)
    {
        credentials = string.Empty;

        if (!headers.TryGetValue(AuthorizationHeaderName, out var encodedCredentials) || string.IsNullOrEmpty(encodedCredentials))
        {
            _loggingService.LogInfo("Authorization header was not found.");

            return false;
        }

        if (!BasicAuthTypeRegex.IsMatch(encodedCredentials))
        {
            _loggingService.LogInfo("Unsupported authorization type. Only basic authorization type is supported.");

            return false;
        }

        credentials = BasicAuthTypeRegex.Replace(encodedCredentials, "");

        if (!credentials.IsBase64())
        {
            credentials = string.Empty;

            _loggingService.LogInfo("Basic authorization credentials must be Base64 encoded.");

            return false;
        }

        return true;
    }

    public bool CheckPassword(User user, string password)
    {
        return user.Password == password.ToSha256();
    }
}