echo '********************************************Executing tests********************************************'
        
echo '********************************************net8.0 tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Features\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Edge|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Xunit\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Edge|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json 
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.MsTest\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Edge|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.UnitTests\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Edge|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json

dotnet vstest .\Ocaramba.Tests.Features\bin\Release\net8.0\Ocaramba.Tests.Features.dll .\Ocaramba.Tests.Xunit\bin\Release\net8.0\Ocaramba.Tests.Xunit.dll D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.MsTest\bin\Release\net8.0\Ocaramba.Tests.MsTest.dll D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.UnitTests\bin\Release\net8.0\Ocaramba.UnitTests.dll /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"


echo '********************************************EdgeChrominum tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToEdgeChromiumDriverDirectory" "EdgeChromium|C:\SeleniumWebDrivers\EdgeDriver" -logValues -json
        

dotnet vstest .\Ocaramba.Tests.NUnit\bin\Release\net8.0\Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory=Grid)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.EdgeChrominum.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0