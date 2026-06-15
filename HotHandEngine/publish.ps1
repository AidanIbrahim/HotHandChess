param(
    [Parameter(Mandatory=$true)]
    [string]$Name
)

$OutputDir = "./engines"
$Project = "./src/HotHandEngine.csproj"

# Ensure engines folder exists
if (!(Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir | Out-Null
}

# Publish the engine
dotnet publish $Project `
    -c Release `
    -r win-x64 `
    --self-contained true `
    /p:PublishSingleFile=true `
    /p:AssemblyName=$Name `
    -o $OutputDir

Write-Host "Engine published as $OutputDir/$Name.exe"
