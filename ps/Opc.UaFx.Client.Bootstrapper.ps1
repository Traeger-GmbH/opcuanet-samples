[CmdletBinding()]
Param(
    [Parameter(Mandatory = $false)]
    [switch]$Silent
)

$packageSource = "https://www.nuget.org/api/v2"
$packageDestination = "./packages"

function Invoke-Bootstrap()
{
    if (!$Silent) {
        Write-Host "Preparing packages..." -ForegroundColor Yellow
    }

    Submit-Package -Name Portable.BouncyCastle -Version 1.8.1.3 -Framework netstandard2.0
    Submit-Package -Name System.Reflection.DispatchProxy -Version 4.5.0 -Framework netstandard2.0
    Submit-Package -Name System.ServiceModel.Primitives -Version 4.5.3 -Framework netstandard2.0
    Submit-Package -Name System.Private.ServiceModel -Version 4.5.3 -Framework netstandard2.0
    Submit-Package -Name Opc.UaFx.Client -Version 2.8.3.1 -Framework netstandard2.0

    if (!$Silent) {
        Write-Host ""
    }
}

function Submit-Package($Name, $Version, $Framework)
{
    if (!$Silent) {
        Write-Host "`Preparing '$Name'..." -ForegroundColor DarkGray -NoNewLine
    }

    $packagePath = $packageDestination + "/" + $Name + "." + $Version;

    if (!(Test-Path $packagePath -PathType Container)) {
        $package = Find-Package `
            -Name $Name `
            -RequiredVersion $Version `
            -Source $packageDestination `
            -ProviderName NuGet `
            -Force `
            -ForceBootstrap `
            -ErrorAction Ignore
            
        if (!$package) {
            Install-Package `
                -Name $Name `
                -RequiredVersion $Version `
                -Source $packageSource `
                -Destination $packageDestination `
                -SkipDependencies `
                -ProviderName NuGet `
                -Force `
                -ForceBootstrap > $null
        }
    }

    if (Test-Path ($packagePath + "/lib/" + $Framework) -PathType Container) {
        $packagePath += "/lib/" + $Framework + "/";
    }
    else {
        $packagePath += "/runtimes/win/lib/" + $Framework + "/";
    }

    $assemblyFiles = Get-ChildItem -Path $packagePath -Filter *.dll

    foreach ($assemblyFile in $assemblyFiles) {
        try {
            Add-Type -LiteralPath $assemblyFile.FullName
        }
        catch {
            Write-Error "$($_.Exception.GetType().FullName): $($_.Exception.Message)"

            if ($_.Exception -is [System.Reflection.ReflectionTypeLoadException]) {
                for ($i = 0 ; $i -lt $_.Exception.LoaderExceptions.Length ; $i++) {
                    $loaderException = $_.Exception.LoaderExceptions[$i]
                    Write-Error "LoaderExceptions[$i]: $($loaderException.GetType().FullName): $($loaderException.Message)"
                }
            }

            return
        }
    }

    if (!$Silent) {
        Write-Host " prepared." -ForegroundColor DarkGray
    }
}
