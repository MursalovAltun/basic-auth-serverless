namespace EppendorfAuth.Tests;

public class Setup : IDisposable
{
    public static string ApiUrl { get; set; } = "https://1anwy7onya.execute-api.eu-north-1.amazonaws.com/dev";

    public Setup()
    {
    }

    public void Dispose()
    {
    }
}