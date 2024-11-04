Set-Location $PSScriptRoot
Set-Location ..
dotnet test --logger:"junit" --collect:"XPlat Code Coverage"
