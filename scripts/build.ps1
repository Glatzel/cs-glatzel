Set-Location $PSScriptRoot
Set-Location ..
Remove-Item ./bin -Recurse -ErrorAction SilentlyContinue


msbuild /p:Configuration=Release /p:Platform=x64 /p:DebugSymbols=false /p:DebugType=None /t:Glatzel /restore:true /maxcpucount

