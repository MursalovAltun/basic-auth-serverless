using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using EppendorfAuthApi.Core;

namespace EppendorfAuthApi.Implementations;

public class DynamoDbUserRepository : IUserRepository
{
    private readonly AmazonDynamoDBClient _client;
    private readonly static string USERS_TABLE_NAME = Environment.GetEnvironmentVariable("USERS_TABLE_NAME") ?? "";

    public DynamoDbUserRepository(AmazonDynamoDBClient client)
    {
        _client = client;
    }

    public async Task<User?> GetUserAsync(string username)
    {
        var getUserResponse = await _client.GetItemAsync(new GetItemRequest(USERS_TABLE_NAME,
            new Dictionary<string, AttributeValue>(1)
            {
                {nameof(User.Username), new AttributeValue(username)}
            }));

        if (!getUserResponse.IsItemSet)
        {
            return null;
        }

        return User.Create(getUserResponse.Item[nameof(User.Username)].S, getUserResponse.Item[nameof(User.Password)].S);
    }
}