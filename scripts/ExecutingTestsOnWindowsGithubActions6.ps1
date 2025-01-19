echo '********************************************CloudProviderCrossBrowser tests********************************************'

echo '********************************************saucelabs tests********************************************'
    
.\Ocaramba\set_AppConfig_for_tests.ps1 ".\Ocaramba\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.json" "appSettings" "RemoteWebDriverHub" "https://$($env:MAPPED_ENV_SAUCELABSUSERNAME):$($env:MAPPED_ENV_SAUCELABSACCESSKEY)@ondemand.us-west-1.saucelabs.com:443/wd/hub" -json
    
.\Ocaramba\set_AppConfig_for_tests.ps1 ".\Ocaramba\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.json" "DriverCapabilities" "buildName" "Ocaramba.Tests.SauceLabsCrossBrowser$($env:BuildVersion)" -logValues -json
    
dotnet vstest .\Ocaramba\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0\Ocaramba.Tests.CloudProviderCrossBrowser.dll /TestCaseFilter:"(FullyQualifiedName!~Iphone)&(FullyQualifiedName!~Android)&(FullyQualifiedName!~Samsung)" /Logger:"trx;LogFileName=Ocaramba.Tests.saucelabsnet8.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0    
