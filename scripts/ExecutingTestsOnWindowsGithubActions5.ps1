$Env:ASPNETCORE_ENVIRONMENT="Linux"

echo $Env:ASPNETCORE_ENVIRONMENT

echo '********************************************Run tests with Selenium Grid ****************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net8.0" "appsettings.Linux.json" "appSettings" "browser|RemoteWebDriverHub" "RemoteWebDriver|http://localhost:4444/wd/hub" -json -logValues

dotnet vstest ./Ocaramba.Tests.NUnit/bin/Release/net8.0/Ocaramba.Tests.NUnit.dll /TestCaseFilter:"TestCategory=Grid" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.NUnitGrid.xml"

docker ps -q | ForEach-Object {
    Write-Host "==== Logs for container: $_ ===="
    docker logs $_
}

Compress-Archive -Path ".\Ocaramba.Tests.NUnit\bin\Release\net8.0\TestOutput\*" -DestinationPath "WindowsCore5$env:GITHUB_RUN_ID.zip"

if($lastexitcode -ne 0)
{
  echo 'lastexitcode' $lastexitcode
}

exit 0
