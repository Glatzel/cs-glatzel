Set-Location $PSScriptRoot
Set-Location ..
dotnet test --logger:junit --collect:"XPlat Code Coverage"

try {dotnet-coverage}
catch {dotnet tool install --global dotnet-coverage}

dotnet-coverage merge tests/**/TestResults/*/coverage.cobertura.xml  --output coverage.xml --output-format xml
