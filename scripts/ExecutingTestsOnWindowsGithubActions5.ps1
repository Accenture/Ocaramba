echo '********************************************Downloading Selenium Grid********************************************'

docker network create grid

docker run -d -p 4442-4444:4442-4444 --net grid --name selenium-hub selenium/hub:latest
docker run -d --net grid -e SE_EVENT_BUS_HOST=selenium-hub --shm-size="2g" -e SE_EVENT_BUS_PUBLISH_PORT=4442 -e SE_EVENT_BUS_SUBSCRIBE_PORT=4443  selenium/node-chrome:latest

# Wait for Selenium Grid to be ready
$maxAttempts = 20
$attempt = 0
$gridReady = $false

while (-not $gridReady -and $attempt -lt $maxAttempts) {
    try {
        $response = Invoke-WebRequest -Uri "http://localhost:4444/status" -UseBasicParsing -TimeoutSec 5
        if ($response.StatusCode -eq 200) {
            $json = $response.Content | ConvertFrom-Json
            if ($json.value.ready -eq $true) {
                $gridReady = $true
                Write-Host "Selenium Grid is UP!"
            } else {
                Write-Host "Selenium Grid not ready yet, waiting..."
            }
        }
    } catch {
        Write-Host "Waiting for Selenium Grid..."
    }
    Start-Sleep -Seconds 3
    $attempt++
}

if (-not $gridReady) {
    Write-Error "Selenium Grid did not start in time."
    exit 1
}

$Env:ASPNETCORE_ENVIRONMENT="Linux"

echo $Env:ASPNETCORE_ENVIRONMENT

echo '********************************************Run tests with Selenium Grid ****************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net8.0" "appsettings.Linux.json" "appSettings" "browser|RemoteWebDriverHub" "RemoteWebDriver|http://localhost:4444/wd/hub" -json -logValues

dotnet vstest ./Ocaramba.Tests.NUnit/bin/Release/net8.0/Ocaramba.Tests.NUnit.dll /TestCaseFilter:"TestCategory=Grid" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.NUnitGrid.xml"

echo '*****************************Run CloudProviderCrossBrowser tests with Selenium Grid****************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.Linux.json" "appSettings" "RemoteWebDriverHub" "http://localhost:4444/wd/hub" -json -logValues

dotnet vstest ./Ocaramba.Tests.CloudProviderCrossBrowser/bin/Release/net8.0/Ocaramba.Tests.CloudProviderCrossBrowser.dll /TestCaseFilter:"FullyQualifiedName~Chrome" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.CloudProviderCrossBrowserGrid.xml"

$staging = "TempZipStaging"
New-Item -ItemType Directory -Path $staging -Force

Copy-Item ".\Ocaramba.Tests.NUnit\bin\Release\net8.0\TestOutput" -Destination "$staging\Ocaramba.Tests.NUnit" -Recurse
Copy-Item ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0\TestOutput" -Destination "$staging\Ocaramba.CloudProviderCrossBrowser" -Recurse
Compress-Archive -Path "$staging\*" -DestinationPath "WindowsCore5$env:GITHUB_RUN_ID.zip"
Remove-Item -Recurse -Force $staging


if($lastexitcode -ne 0)
{
  echo 'lastexitcode' $lastexitcode
}

exit 0
