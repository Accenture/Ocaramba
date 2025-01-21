echo '********************************************Downloading Selenium Grid********************************************'

docker network create grid

docker run -d -p 4442-4444:4442-4444 --net grid --name selenium-hub selenium/hub:latest
docker run -d --net grid -e SE_EVENT_BUS_HOST=selenium-hub --shm-size="2g" -e SE_EVENT_BUS_PUBLISH_PORT=4442 -e SE_EVENT_BUS_SUBSCRIBE_PORT=4443  selenium/node-chrome:latest

$retryCount = 6
$delay = 10
for ($i = 0; $i -lt $retryCount; $i++) {
  try {
    $response = Invoke-RestMethod -Uri "http://localhost:4444/wd/hub/status"
    if ($response.ready) {
      Write-Output "Selenium Grid is ready."
      break
    }
  } catch {
    Write-Output "Attempt $($i + 1) failed. Retrying in $delay seconds..."
    Start-Sleep -Seconds $delay
  }
}
if ($i -eq $retryCount) {
  Write-Output  "Failed to connect to Selenium Grid after $retryCount attempts."
}


echo '********************************************Run tests with Selenium Grid ****************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net8.0\" "appsettings.json" "appSettings" "browser|RemoteWebDriverHub" "RemoteWebDriver|http://localhost:4444/wd/hub" -json -logValues

dotnet vstest .\Ocaramba.Tests.NUnit\bin\Release\net8.0\Ocaramba.Tests.NUnit.dll /TestCaseFilter:"TestCategory=Grid" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.NUnitGrid.xml"

echo '*****************************Run CloudProviderCrossBrowser tests with Selenium Grid****************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.json" "appSettings" "RemoteWebDriverHub" "http://localhost:4444/wd/hub" -json -logValues

dotnet vstest .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0\Ocaramba.Tests.CloudProviderCrossBrowser.dll /TestCaseFilter:"FullyQualifiedName~Chrome" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.CloudProviderCrossBrowserGrid.xml"

if($lastexitcode -ne 0)
{
  echo 'lastexitcode' $lastexitcode
}

exit 0
