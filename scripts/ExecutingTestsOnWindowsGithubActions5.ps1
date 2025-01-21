echo '********************************************Downloading Selenium Grid********************************************'

$url = $env:seleniumGridUrl
$grid = $env:seleniumGridVersion
$output = ".\$grid"
$outputLogs = ".\"

$start_time = Get-Date

echo url: $url
echo grid: $grid
echo output: $output
echo outputLogs: $outputLogs

echo "Downloading Selenium Grid from: $($url) to $($output)"
Invoke-WebRequest -Uri "$($url)" -OutFile "$($output)"

echo "Selenium Grid downloaded to:" $output

echo "Time taken to download $($grid): $((Get-Date).Subtract($start_time).Seconds) second(s)"

echo '******************************************Start Selenium Grid in background****************************************'

java --version

$appHub=Start-Process java -ArgumentList '-jar', $output, 'hub' -RedirectStandardOutput "$outputLogs\console_hub.out" -RedirectStandardError "$outputLogs\console_hub.err" -PassThru

Start-Sleep -s 5

echo "Selenium Grid hub started"

$appNode=Start-Process java -ArgumentList '-jar', $output, 'node --detect-drivers true' -RedirectStandardOutput "$outputLogs\console_node.out" -RedirectStandardError "$outputLogs\console_node.err" -PassThru

Start-Sleep -s 5

$retryCount = 5
$delay = 5
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
echo "Selenium Grid node started"

echo '********************************************Run tests with Selenium Grid ****************************************'

.\Ocaramba\set_AppConfig_for_tests.ps1 ".\Ocaramba\Ocaramba.Tests.NUnit\bin\Release\net8.0\" "appsettings.json" "appSettings" "browser|RemoteWebDriverHub" "RemoteWebDriver|http://localhost:4444/wd/hub" -json

dotnet vstest .\Ocaramba\Ocaramba.Tests.NUnit\bin\Release\net8.0\Ocaramba.Tests.NUnit.dll /TestCaseFilter:"TestCategory=Grid" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.NUnitGrid.xml"

echo '*****************************Run CloudProviderCrossBrowser tests with Selenium Grid****************************'

.\Ocaramba\set_AppConfig_for_tests.ps1 ".\Ocaramba\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.json" "appSettings" "RemoteWebDriverHub" "http://localhost:4444/wd/hub" -json

dotnet vstest .\Ocaramba\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0\Ocaramba.Tests.CloudProviderCrossBrowser.dll /TestCaseFilter:"FullyQualifiedName~Chrome" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.CloudProviderCrossBrowserGrid.xml"

if($lastexitcode -ne 0)
{
  echo 'lastexitcode' $lastexitcode
}

echo '*****************************Stop Selenium Grid****************************'

echo "Stop Selenium Grid node"

Stop-Process -Id $appNode.Id

echo "Stop Selenium Grid hub"

Stop-Process -Id $appHub.Id

if($lastexitcode -ne 0)
{
  echo 'lastexitcode' $lastexitcode
}

exit 0
