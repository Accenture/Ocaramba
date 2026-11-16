echo '********************************************Executing tests********************************************'
        
echo '********************************************.NET tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Features\bin\Release" "appsettings.json" "appSettings" "browser" "Edge" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Xunit\bin\Release" "appsettings.json" "appSettings" "browser" "Edge" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.MsTest\bin\Release" "appsettings.json" "appSettings" "browser" "Edge" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.UnitTests\bin\Release" "appsettings.json" "appSettings" "browser" "Edge" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release" "appsettings.json" "appSettings" "browser" "EdgeChromium|C:\SeleniumWebDrivers\EdgeDriver" -logValues -json

echo '********************************************Features tests********************************************'
dotnet vstest .\Ocaramba.Tests.Features\bin\Release\Ocaramba.Tests.Features.dll  /TestCaseFilter:"(TestCategory!=TakingScreenShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.Features.xml"

echo '********************************************MsTest tests********************************************'
dotnet vstest .\Ocaramba.Tests.MsTest\bin\Release\Ocaramba.Tests.MsTest.dll /TestCaseFilter:"(TestCategory!=TakingScreenShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.MsTest.xml"

echo '********************************************Xunit tests********************************************'
dotnet vstest .\Ocaramba.Tests.Xunit\bin\Release\Ocaramba.Tests.Xunit.dll  /TestCaseFilter:"(TestCategory!=TakingScreenShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.Xunit.xml"

echo '********************************************UnitTests tests********************************************'
dotnet vstest .\Ocaramba.UnitTests\bin\Release\Ocaramba.UnitTests.dll  /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.UnitTests.xml"

echo '********************************************EdgeChromium NUnit tests********************************************'

dotnet vstest .\Ocaramba.Tests.NUnit\bin\Release\Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory=Grid)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.EdgeChromium.xml"

$staging = "TempZipStaging_Core1"
New-Item -ItemType Directory -Path $staging -Force

Copy-Item ".\Ocaramba.Tests.Features\bin\Release\TestOutput" -Destination "$staging\Ocaramba.Tests.Features" -Recurse
Copy-Item ".\Ocaramba.Tests.Xunit\bin\Release\TestOutput" -Destination "$staging\Ocaramba.Tests.Xunit" -Recurse
Copy-Item ".\Ocaramba.Tests.MsTest\bin\Release\TestOutput" -Destination "$staging\Ocaramba.Tests.MsTest" -Recurse
Copy-Item ".\Ocaramba.UnitTests\bin\Release\TestOutput" -Destination "$staging\Ocaramba.UnitTests" -Recurse
Copy-Item ".\Ocaramba.Tests.NUnit\bin\Release\TestOutput" -Destination "$staging\Ocaramba.Tests.NUnit" -Recurse
Compress-Archive -Path "$staging\*" -DestinationPath "WindowsCore2$env:GITHUB_RUN_ID.zip"
Remove-Item -Recurse -Force $staging

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0