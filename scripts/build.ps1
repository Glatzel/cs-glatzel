Set-Location $PSScriptRoot
Set-Location ..
Remove-Item ./bin -Recurse -ErrorAction SilentlyContinue

dotnet build --configuration Release
