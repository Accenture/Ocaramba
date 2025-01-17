echo '********************************************Executing tests********************************************'
        
echo '********************************************net8.0 tests********************************************'

$Env:ASPNETCORE_ENVIRONMENT="Linux"

echo $Env:ASPNETCORE_ENVIRONMENT

$chromeDriver = Get-ChildItem -Path "/opt/hostedtoolcache/setup-chrome/chromedriver/" -Recurse -Filter "chromedriver" | Select-Object -First 1
$chromeDriverPath = Split-Path -Path $chromeDriver.FullName

$chrome = Get-ChildItem -Path "/opt/hostedtoolcache/setup-chrome/chromium/" -Recurse -Filter "chrome" | Select-Object -First 1
$chromePath = Split-Path -Path $chrome.FullName


.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net8.0" "appsettings.Linux.json" "appSettings" "browser|PathToChromeDriverDirectory|ChromeBrowserExecutableLocation" "Chrome|$chromeDriverPath|$chromePath" -logValues -json

dotnet vstest ./Ocaramba.Tests.NUnit/bin/Release/net8.0/Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory!=NotImplementedInCoreOrUploadDownload)" /Parallel --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
