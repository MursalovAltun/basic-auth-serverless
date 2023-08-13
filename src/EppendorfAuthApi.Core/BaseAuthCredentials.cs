using System.Text;

namespace EppendorfAuthApi.Core;

public class BaseAuthCredentials
{
    private const char BaseAuthCredentialsSplitter = ':';

    public static BaseAuthCredentials? FromBase64EncodedCredentials(string base64Credentials)
    {
        var credentialsDecoded = Encoding.UTF8.GetString(Convert.FromBase64String(base64Credentials));

        var credentialsParts = credentialsDecoded.Split(BaseAuthCredentialsSplitter);

        if (credentialsParts.Length != 2)
        {
            return null;
        }

        var username = credentialsParts[0];
        var password = credentialsParts[1];

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return null;
        }

        return new BaseAuthCredentials
        {
            Username = username,
            Password = password
        };
    }

    private BaseAuthCredentials() { }

    internal BaseAuthCredentials(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; private set; } = "";
    public string Password { get; private set; } = "";
}