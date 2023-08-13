namespace EppendorfAuthApi.Core;

public interface IAuthService
{
    bool AreCredentialsPresentAndValid(IDictionary<string, string> headers, out string credentials);

    bool CheckPassword(User user, string password);
}