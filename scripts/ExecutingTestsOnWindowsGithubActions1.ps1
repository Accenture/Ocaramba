echo '********************************************Executing tests********************************************'
        
echo '********************************************net8.0 tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba.Tests.NUnit\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba.Tests.Angular\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba.Tests.NUnitExtentReports\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json

dotnet vstest D:\a\Ocaramba\Ocaramba\Ocaramba.Tests.Angular\bin\Release\net8.0\Ocaramba.Tests.Angular.dll D:\a\Ocaramba\Ocaramba\Ocaramba.Tests.NUnit\bin\Release\net8.0\Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.trx"

echo '********************************************net472 tests********************************************'

#.\scripts\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.Features\bin\Release\net472" "Ocaramba.Tests.Features.dll.config" "appsettings.json" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues

#vstest.console.exe D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.Features\bin\Release\net472\Ocaramba.Tests.Features.dll /Logger:"trx;LogFileName=Ocaramba.Tests.Features.trx"
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0