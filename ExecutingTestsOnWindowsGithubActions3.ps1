echo '********************************************Executing tests********************************************'
        
echo '********************************************net472 tests*********************************************'

.\scripts\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.Angular\bin\Release\net472" "Ocaramba.Tests.Angular.dll.config" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues
.\scripts\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.NUnit\bin\Release\net472" "Ocaramba.Tests.NUnit.dll.config" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues
.\scripts\set_AppConfig_for_tests.ps1 "D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.UnitTests\bin\Release\net472" "Ocaramba.UnitTests.dll.config" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|C:\SeleniumWebDrivers\ChromeDriver" -logValues


vstest.console.exe D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.Angular\bin\Release\net472\Ocaramba.Tests.Angular.dll D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.NUnit\bin\Release\net472\Ocaramba.Tests.NUnit.dll D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.UnitTests\bin\Release\net472\Ocaramba.UnitTests.dll /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.net4.xml"


if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
