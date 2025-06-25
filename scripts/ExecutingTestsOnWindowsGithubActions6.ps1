echo '********************************************CloudProviderCrossBrowser tests********************************************'

echo '********************************************saucelabs tests********************************************'
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.json" "appSettings" "RemoteWebDriverHub" "https://$($env:MAPPED_ENV_SAUCELABSUSERNAME):$($env:MAPPED_ENV_SAUCELABSACCESSKEY)@ondemand.us-west-1.saucelabs.com:443/wd/hub" -json
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.json" "DriverCapabilities" "buildName" "Ocaramba.Tests.SauceLabsCrossBrowser$($env:BuildVersion)" -logValues -json
    
dotnet vstest .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0\Ocaramba.Tests.CloudProviderCrossBrowser.dll  /Logger:"trx;LogFileName=Ocaramba.Tests.saucelabsnet8.xml"

Compress-Archive -Path "./Ocaramba.Tests.CloudProviderCrossBrowser/bin/Release/net8.0/TestOutput/*" -DestinationPath "./Ocaramba.Tests.CloudProviderCrossBrowser/bin/Release/net8.0/ExecutingTestsOnsaucelabLinux$env:GITHUB_RUN_ID.zip"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0    
