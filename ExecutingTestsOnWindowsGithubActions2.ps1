echo '********************************************Executing tests********************************************'
        
echo '********************************************net6.0 tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.Features\bin\Release\net6.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.Xunit\bin\Release\net6.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json 
.\scripts\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.MsTest\bin\Release\net6.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.UnitTests\bin\Release\net6.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json

dotnet vstest D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.Features\bin\Release\net6.0\Ocaramba.Tests.Features.dll D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.Xunit\bin\Release\net6.0\Ocaramba.Tests.Xunit.dll D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.MsTest\bin\Release\net6.0\Ocaramba.Tests.MsTest.dll D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.UnitTests\bin\Release\net6.0\Ocaramba.UnitTests.dll /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"


echo '********************************************EdgeChrominum tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.NUnit\bin\Release\net6.0" "appsettings.json" "appSettings" "browser|C:\SeleniumWebDrivers\EdgeDriver" "EdgeChromium|$($env:EDGEWEBDRIVER)" -logValues -json
        

dotnet vstest D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.NUnit\bin\Release\net6.0\Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory=Grid)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.EdgeChrominum.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0