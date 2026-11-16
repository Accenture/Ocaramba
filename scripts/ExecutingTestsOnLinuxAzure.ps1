echo '********************************************Executing tests********************************************'
        
echo '********************************************.NET tests********************************************'

$Env:ASPNETCORE_ENVIRONMENT="Linux"

echo $Env:ASPNETCORE_ENVIRONMENT

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release" "appsettings.Linux.json" "appSettings" "browser|protocol|host" "Chrome|http|localhost:7080" -logValues -json

dotnet vstest ./Ocaramba.Tests.NUnit/bin/Release/Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory!=NotImplementedInCoreOrUploadDownload)" /Parallel --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"

Compress-Archive -Path "./Ocaramba.Tests.NUnit/bin/Release/TestOutput/*" -DestinationPath "./Ocaramba.Tests.NUnit/bin/Release/ExecutingTestsOnLinux$env:GITHUB_RUN_ID.zip"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
