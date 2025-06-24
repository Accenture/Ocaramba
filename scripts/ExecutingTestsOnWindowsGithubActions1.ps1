echo '********************************************Executing tests********************************************'

echo '********************************************net8.0 tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Edge|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Angular\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Edge|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnitExtentReports\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Edge|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json

dotnet vstest .\Ocaramba.Tests.Angular\bin\Release\net8.0\Ocaramba.Tests.Angular.dll .\Ocaramba.Tests.NUnit\bin\Release\net8.0\Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"

dotnet vstest .\Ocaramba.Tests.NUnitExtentReports\bin\Release\net8.0\Ocaramba.Tests.NUnitExtentReports.dll /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.NUnitExtentReports.xml"

$staging = "TempZipStaging_Core1"
New-Item -ItemType Directory -Path $staging -Force
Copy-Item ".\Ocaramba.Tests.NUnit\bin\Release\net8.0\TestOutput" -Destination "$staging\Ocaramba.Tests.NUnit" -Recurse
Copy-Item ".\Ocaramba.Tests.Angular\bin\Release\net8.0\TestOutput" -Destination "$staging\Ocaramba.Tests.Angular" -Recurse
Copy-Item ".\Ocaramba.Tests.NUnitExtentReports\bin\Release\net8.0\TestOutput" -Destination "$staging\Ocaramba.Tests.NUnitExtentReports" -Recurse
Compress-Archive -Path "$staging\*" -DestinationPath "WindowsCore1$env:GITHUB_RUN_ID.zip"
 Remove-Item -Recurse -Force $staging

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
