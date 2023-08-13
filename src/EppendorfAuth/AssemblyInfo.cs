using System.Runtime.CompilerServices;
using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
[assembly: InternalsVisibleTo("EppendorfAuth.Tests")]