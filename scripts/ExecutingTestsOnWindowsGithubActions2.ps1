echo '********************************************Executing tests********************************************'
        
echo '********************************************net8.0 tests********************************************'

.\Ocaramba\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.Features\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json
.\Ocaramba\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.Xunit\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json 
.\Ocaramba\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.MsTest\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json
.\Ocaramba\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.UnitTests\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues -json

dotnet vstest D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.Features\bin\Release\net8.0\Ocaramba.Tests.Features.dll D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.Xunit\bin\Release\net8.0\Ocaramba.Tests.Xunit.dll D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.MsTest\bin\Release\net8.0\Ocaramba.Tests.MsTest.dll D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.UnitTests\bin\Release\net8.0\Ocaramba.UnitTests.dll /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"


echo '********************************************EdgeChrominum tests********************************************'

.\Ocaramba\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.NUnit\bin\Release\net8.0" "appsettings.json" "appSettings" "browser|PathToEdgeChromiumDriverDirectory" "EdgeChromium|C:\SeleniumWebDrivers\EdgeDriver" -logValues -json
        

dotnet vstest D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.NUnit\bin\Release\net8.0\Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory=Grid)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.EdgeChrominum.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0