dotnet restore
dotnet publish src/EppendorfAuth --configuration "Release" --framework "net6.0" //p:GenerateRuntimeConfigurationFiles=true --runtime linux-x64 --self-contained false
terraform apply -auto-approve