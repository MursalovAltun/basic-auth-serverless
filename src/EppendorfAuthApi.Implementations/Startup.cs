using Amazon.DynamoDBv2;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using EppendorfAuthApi.Core;
using Microsoft.Extensions.DependencyInjection;

namespace EppendorfAuthApi.Implementations;

public static class Startup
{
    public static ServiceProvider Services { get; private set; } = default!;

    public static void ConfigureServices()
    {
        var serviceCollection = new ServiceCollection();

        AWSSDKHandler.RegisterXRayForAllServices();

        if (AWSXRayRecorder.IsLambda())
            serviceCollection.AddSingleton<ILoggingService>(new SerilogLogger(AWSXRayRecorder.Instance.GetEntity().TraceId));

        serviceCollection.AddSingleton(new AmazonDynamoDBClient(new AmazonDynamoDBConfig()));
        serviceCollection.AddTransient<IUserRepository, DynamoDbUserRepository>();
        serviceCollection.AddTransient<IAuthService, BasicAuthService>();

        Services = serviceCollection.BuildServiceProvider();
    }
}