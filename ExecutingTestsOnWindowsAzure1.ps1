echo '********************************************Executing tests********************************************'
        
echo '********************************************netcoreapp3 tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\netcoreapp3.1" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$($env:CHROMEWEBDRIVER)" $true $true
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Angular\bin\Release\netcoreapp3.1" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$($env:CHROMEWEBDRIVER)" $true $true
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnitExtentReports\bin\Release\netcoreapp3.1" "appsettings.json" "browser|PathToChromeDriverDirectory" "PathToChromeDriverDirectory" "Chrome|$($env:CHROMEWEBDRIVER)" $true $true

dotnet vstest .\Ocaramba.Tests.Angular\bin\Release\netcoreapp3.1\Ocaramba.Tests.Angular.dll `
	          .\Ocaramba.Tests.NUnit\bin\Release\netcoreapp3.1\Ocaramba.Tests.NUnit.dll `
			  .\Ocaramba.Tests.NUnitExtentReports\bin\Release\netcoreapp3.1\Ocaramba.Tests.NUnitExtentReports.dll `
			  /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel `
	          --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.trx"

echo '********************************************net472 tests********************************************'

$vstest = (Resolve-Path "D:\a\_temp\VsTest\Microsoft.TestPlatform*\tools\net*\Common*\IDE\Extensions\TestPlatform\vstest.console.exe").ToString()
& $vstest .\Ocaramba.Tests.Features\bin\Release\net472\Ocaramba.Tests.Features.dll .\Ocaramba.Tests.NUnit\bin\Release\net45\Ocaramba.Tests.NUnit.dll .\Ocaramba.Tests.NUnitExtentReports\bin\Release\netcoreapp3.1\Ocaramba.Tests.NUnitExtentReports.dll --logger:"trx;LogFileName=Ocaramba.Tests.Features.trx"
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
