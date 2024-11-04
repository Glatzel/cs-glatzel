Set-Location $PSScriptRoot
Set-Location ..
dotnet test --logger:"junit;LogFilePath=junit.xml"
