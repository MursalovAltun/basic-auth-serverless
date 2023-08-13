namespace EppendorfAuthApi.Core;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string username);
}