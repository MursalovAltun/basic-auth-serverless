using System.Net;
using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;

namespace EppendorfAuthApi.Implementations;

public class BaseApiIntegratedHttpFunction
{
    public APIGatewayProxyResponse NotAllowed()
    {
        return new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.MethodNotAllowed
        };
    }

    public APIGatewayProxyResponse Ok()
    {
        return new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK
        };
    }

    public APIGatewayProxyResponse BadRequest()
    {
        return new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.BadRequest
        };
    }

    public APIGatewayProxyResponse BadRequest(string message)
    {
        return new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } },
            Body = JsonSerializer.Serialize(new
            {
                message,
            })
        };
    }
}