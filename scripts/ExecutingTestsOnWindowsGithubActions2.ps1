echo '********************************************Executing tests********************************************'
        
echo '********************************************net8.0 tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Features\bin\Release\net8.0" "appsettings.json" "appSettings" "browser" "Edge" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Xunit\bin\Release\net8.0" "appsettings.json" "appSettings" "browser" "Edge" -logValues -json 
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.MsTest\bin\Release\net8.0" "appsettings.json" "appSettings" "browser" "Edge" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.UnitTests\bin\Release\net8.0" "appsettings.json" "appSettings" "browser" "Edge" -logValues -json

dotnet vstest .\Ocaramba.Tests.Features\bin\Release\net8.0\Ocaramba.Tests.Features.dll /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.Features.xml"

dotnet vstest .\Ocaramba.Tests.Xunit\bin\Release\net8.0\Ocaramba.Tests.Xunit.dll  /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.Xunit.xml"

dotnet vstest .\Ocaramba.Tests.MsTest\bin\Release\net8.0\Ocaramba.Tests.MsTest.dll  /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.MsTest.xml"

dotnet vstest .\Ocaramba.UnitTests\bin\Release\net8.0\Ocaramba.UnitTests.dll  /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.UnitTests.xml"


echo '********************************************EdgeChrominum tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net8.0" "appsettings.json" "appSettings" "browser" "EdgeChromium|C:\SeleniumWebDrivers\EdgeDriver" -logValues -json
        

dotnet vstest .\Ocaramba.Tests.NUnit\bin\Release\net8.0\Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory=Grid)" /Parallel /Logger:"trx;LogFileName=Ocaramba.Tests.EdgeChrominum.xml"

$staging = "TempZipStaging_Core1"
New-Item -ItemType Directory -Path $staging -Force

Copy-Item ".\Ocaramba.Tests.Features\bin\Release\net8.0\TestOutput" -Destination "$staging\Ocaramba.Tests.Features" -Recurse
Copy-Item ".\Ocaramba.Tests.Xunit\bin\Release\net8.0\TestOutput" -Destination "$staging\Ocaramba.Tests.Xunit" -Recurse
Copy-Item ".\Ocaramba.Tests.MsTest\bin\Release\net8.0\TestOutput" -Destination "$staging\Ocaramba.Tests.MsTest" -Recurse
Copy-Item ".\Ocaramba.UnitTests\bin\Release\net8.0\TestOutput" -Destination "$staging\Ocaramba.UnitTests" -Recurse
Copy-Item ".\Ocaramba.Tests.NUnit\bin\Release\net8.0\TestOutput" -Destination "$staging\Ocaramba.Tests.NUnit" -Recurse
Compress-Archive -Path "$staging\*" -DestinationPath "WindowsCore2$env:GITHUB_RUN_ID.zip"
Remove-Item -Recurse -Force $staging

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0