resource "aws_dynamodb_table" "eppendorf_auth_api_table" {
  name         = var.table_name
  billing_mode = "PAY_PER_REQUEST"
  hash_key     = "Username"

  attribute {
    name = "Username"
    type = "S"
  }
}

# Create S3 bucket to store our application source code.
resource "aws_s3_bucket" "lambda_bucket" {
  bucket = var.code_bucket_name

  acl           = "private"
  force_destroy = true
}

# Initialize module containing IAM policies.
module "iam_policies" {
  source     = "./tf-modules/iam-policies"
  table_name = aws_dynamodb_table.eppendorf_auth_api_table.name
}


module "eppendorf_auth_lambda" {
  source           = "./tf-modules/lambda-function"
  lambda_bucket_id = aws_s3_bucket.lambda_bucket.id
  publish_dir      = "${path.module}/src/EppendorfAuth/bin/Release/net6.0/linux-x64/publish"
  zip_file         = "EppendorfAuth.zip"
  function_name    = "EppendorfAuth"
  lambda_handler   = "EppendorfAuth::EppendorfAuth.Function::FunctionHandler"
  environment_variables = {
    "USERS_TABLE_NAME" = aws_dynamodb_table.eppendorf_auth_api_table.name
  }
}

resource "aws_iam_role_policy_attachment" "eppendorf_auth_lambda_dynamo_db_read" {
  role       = module.eppendorf_auth_lambda.function_role_name
  policy_arn = module.iam_policies.dynamo_db_read
}

module "api_gateway" {
  source            = "./tf-modules/api-gateway"
  api_name          = "eppendorf-auth-api"
  stage_name        = "dev"
  stage_auto_deploy = true

  api_specification = templatefile("api.spec.yaml", { api_url = "", eppendorf_auth_lambda_arn = module.eppendorf_auth_lambda.function_arn })
}

resource "aws_lambda_permission" "auth_lambda_api_gw" {
  statement_id  = "AllowLambdaExecutionFromAPIGateway_${module.eppendorf_auth_lambda.function_name}"
  action        = "lambda:InvokeFunction"
  function_name = module.eppendorf_auth_lambda.function_name
  principal     = "apigateway.amazonaws.com"

  source_arn = "${module.api_gateway.api_arn}/*/*"
}
