# Basic auth using serverless design architecture

This is a repository containing an AWS Lambda function and API Gateway for basic authorization and IaC using Terraform.

User credentials are stored in DynamoDB table with password as SHA256 hash.

The function simply receives basic Authorazitaion header thorugh the API Gateway and verifies if provided credentials are correct.

The API Gateway is declared using OpenAPI specification in [api.spec.yaml](https://github.com/MursalovAltun/basic-auth-serverless/blob/main/api.spec.yaml).

The codebase is kept as simple as possible for readability, testability and futher maintenance by others.

## Running tests locally
Prerequisites:
 - [.NET v6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

```bash
dotnet test
```
OR simply use "Run All Tests" lunch configuration if you use [Visual Studio Code](https://code.visualstudio.com/).

## Deployment

Prerequisites:
 - [.NET v6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
 - [Terraform CLI](https://developer.hashicorp.com/terraform/downloads)
 - [AWS CLI](https://docs.aws.amazon.com/cli/latest/userguide/getting-started-install.html) (optional*)

`*` if you do not have AWS CLI installed and configured you must set `access_key` and `secret_key` options for aws provider in [provider.tf](https://github.com/MursalovAltun/basic-auth-serverless/blob/main/provider.tf)

```bash
./deploy.sh
```

## Usage

Test user: username=test, password=test

URL: https://1anwy7onya.execute-api.eu-north-1.amazonaws.com/dev/authorize

```bash
curl --location --request POST 'https://1anwy7onya.execute-api.eu-north-1.amazonaws.com/dev/authorize' \
--header 'Authorization: Basic dGVzdDp0ZXN0'
```

Alternatively you can use any other tool to make an HTTP request.
