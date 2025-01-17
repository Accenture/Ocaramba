echo '********************************************Executing tests********************************************'
        
echo '********************************************net8.0 tests********************************************'

$Env:ASPNETCORE_ENVIRONMENT="Linux"

echo $Env:ASPNETCORE_ENVIRONMENT
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net8.0" "appsettings.Linux.json" "appSettings" "browser|PathToChromeDriverDirectory|ChromeBrowserExecutableLocation" "Chrome|/opt/hostedtoolcache/setup-chrome/chromedriver/1407837/x64/|/opt/hostedtoolcache/setup-chrome/chromium/1407831/x64/" -logValues -json

dotnet vstest ./Ocaramba.Tests.NUnit/bin/Release/net8.0/Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory!=NotImplementedInCoreOrUploadDownload)" /Parallel --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
