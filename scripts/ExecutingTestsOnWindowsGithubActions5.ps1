$Env:ASPNETCORE_ENVIRONMENT="Linux"

echo $Env:ASPNETCORE_ENVIRONMENT

echo '********************************************Run tests with Selenium Grid ****************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net8.0" "appsettings.Linux.json" "appSettings" "browser|RemoteWebDriverHub" "RemoteWebDriver|http://localhost:4444/wd/hub" -json -logValues

dotnet vstest ./Ocaramba.Tests.NUnit/bin/Release/net8.0/Ocaramba.Tests.NUnit.dll /TestCaseFilter:"TestCategory=Grid" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.NUnitGrid.xml"

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.Linux.json" "appSettings" "browser|RemoteWebDriverHub" "RemoteWebDriver|http://localhost:4444/wd/hub" -json -logValues

dotnet vstest ./Ocaramba.Tests.CloudProviderCrossBrowser/bin/Release/net8.0/Ocaramba.Tests.CloudProviderCrossBrowser.dll /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.CloudProviderCrossBrowserGrid.xml"

docker ps -q | ForEach-Object {
    Write-Host "==== Logs for container: $_ ===="
    docker logs $_
}

Copy-Item ".\Ocaramba.Tests.NUnit\bin\Release\net8.0\TestOutput" -Destination "$staging\Ocaramba.Tests.NUnit" -Recurse
Copy-Item ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0\TestOutput" -Destination "$staging\Ocaramba.CloudProviderCrossBrowser" -Recurse
Compress-Archive -Path "$staging\*" -DestinationPath "WindowsCore5$env:GITHUB_RUN_ID.zip"
Remove-Item -Recurse -Force $staging

if($lastexitcode -ne 0)
{
  echo 'lastexitcode' $lastexitcode
}

exit 0
