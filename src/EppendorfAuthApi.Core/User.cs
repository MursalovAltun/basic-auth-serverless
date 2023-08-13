namespace EppendorfAuthApi.Core;

public class User
{
    public static User Create(string username, string password)
    {
        return new User
        {
            Username = username,
            Password = password
        };
    }

    private User() { }

    internal User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; private set; } = "";
    public string Password { get; private set; } = "";
}