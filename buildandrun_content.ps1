dotnet restore src/src.sln
if ($LastExitCode -ne 0) { return; }

dotnet build src/src.sln --no-restore
if ($LastExitCode -ne 0) { return; }

# Run the 2 console apps in different windows

Start-Process "dotnet" -ArgumentList "run --project src/Services/Content/CMSCore.Content.Silo --no-build"
Start-Sleep 10
Start-Process "dotnet" -ArgumentList "run --project src/Web/CMSCore.Content.Api --no-build"