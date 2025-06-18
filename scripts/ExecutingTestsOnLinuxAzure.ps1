echo '********************************************Executing tests********************************************'
        
echo '********************************************net8.0 tests********************************************'

$Env:ASPNETCORE_ENVIRONMENT="Linux"

echo $Env:ASPNETCORE_ENVIRONMENT

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net8.0" "appsettings.Linux.json" "appSettings" "browser" "Chrome" -logValues -json

dotnet vstest ./Ocaramba.Tests.NUnit/bin/Release/net8.0/Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory!=NotImplementedInCoreOrUploadDownload)" /Parallel --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"

Compress-Archive -Path "./Ocaramba.Tests.NUnit/bin/Release/net8.0/TestOutput/*" -DestinationPath "./Ocaramba.Tests.NUnit/bin/Release/net8.0/ExecutingTestsOnLinux.zip"


if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
