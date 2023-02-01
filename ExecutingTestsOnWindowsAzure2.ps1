echo '********************************************Executing tests********************************************'
        
echo '********************************************net6.0 tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Features\bin\Release\net6.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$($env:CHROMEWEBDRIVER)" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Xunit\bin\Release\net6.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$($env:CHROMEWEBDRIVER)" -logValues -json 
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.MsTest\bin\Release\net6.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$($env:CHROMEWEBDRIVER)" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.UnitTests\bin\Release\net6.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$($env:CHROMEWEBDRIVER)" -logValues -json

dotnet vstest .\Ocaramba.Tests.Features\bin\Release\net6.0\Ocaramba.Tests.Features.dll `
			  .\Ocaramba.Tests.Xunit\bin\Release\net6.0\Ocaramba.Tests.Xunit.dll `
			  .\Ocaramba.Tests.MsTest\bin\Release\net6.0\Ocaramba.Tests.MsTest.dll `
	          .\Ocaramba.UnitTests\bin\Release\net6.0\Ocaramba.UnitTests.dll `
			  /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel `
	          --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"


echo '********************************************EdgeChrominum tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net6.0" "appsettings.json" "appSettings" "browser|PathToEdgeChromiumDriverDirectory" "EdgeChromium|$($env:EDGEWEBDRIVER)" -logValues -json
        

dotnet vstest .\Ocaramba.Tests.NUnit\bin\Release\net6.0\Ocaramba.Tests.NUnit.dll `
			  /TestCaseFilter:"(TestCategory=Grid)" /Parallel `
	          --logger:"trx;LogFileName=Ocaramba.Tests.EdgeChrominum.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0