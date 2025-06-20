echo '********************************************Executing tests********************************************'

echo '********************************************net8.0 tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Edge|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Angular\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Edge|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnitExtentReports\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Edge|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json

dotnet vstest .\Ocaramba.Tests.Angular\bin\Release\net8.0\Ocaramba.Tests.Angular.dll .\Ocaramba.Tests.NUnit\bin\Release\net8.0\Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"

dotnet vstest .\Ocaramba.Tests.NUnitExtentReports\bin\Release\net8.0\Ocaramba.Tests.NUnitExtentReports.dll /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.NUnitExtentReports.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
