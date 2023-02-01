echo '********************************************Executing tests********************************************'
        
echo '********************************************net6.0 tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net6.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$($env:CHROMEWEBDRIVER)" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Angular\bin\Release\net6.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$($env:CHROMEWEBDRIVER)" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnitExtentReports\bin\Release\net6.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$($env:CHROMEWEBDRIVER)" -logValues -json

dotnet vstest .\Ocaramba.Tests.Angular\bin\Release\net6.0\Ocaramba.Tests.Angular.dll `
	          .\Ocaramba.Tests.NUnit\bin\Release\net6.0\Ocaramba.Tests.NUnit.dll `
			  .\Ocaramba.Tests.NUnitExtentReports\bin\Release\net6.0\Ocaramba.Tests.NUnitExtentReports.dll `
			  /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel `
	          --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.trx"

echo '********************************************net472 tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Features\bin\Release\net472" "Ocaramba.Tests.Features.dll.config" "//appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$($env:CHROMEWEBDRIVER)" -logValues

$vstest = (Resolve-Path "D:\a\_temp\VsTest\Microsoft.TestPlatform*\tools\net*\Common*\IDE\Extensions\TestPlatform\vstest.console.exe").ToString()
& $vstest .\Ocaramba.Tests.Features\bin\Release\net472\Ocaramba.Tests.Features.dll --logger:"trx;LogFileName=Ocaramba.Tests.Features.trx"
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
