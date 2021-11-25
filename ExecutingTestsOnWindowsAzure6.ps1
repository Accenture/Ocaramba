echo '********************************************CloudProviderCrossBrowser tests********************************************'

$vstest = (Resolve-Path "D:\a\_temp\VsTest\Microsoft.TestPlatform*\tools\net*\Common*\IDE\Extensions\TestPlatform\vstest.console.exe").ToString()
      
   
 
echo '********************************************saucelabs tests********************************************'
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "https://$($env:MAPPED_ENV_SAUCELABSUSERNAME):$($env:MAPPED_ENV_SAUCELABSACCESSKEY)@ondemand.us-west-1.saucelabs.com:443/wd/hub"
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//DriverCapabilities" "buildName" "Ocaramba.Tests.SauceLabsCrossBrowser$($env:BuildVersion)" -logValues   
    
& $vstest .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll /TestCaseFilter:"(FullyQualifiedName!~Iphone)&(FullyQualifiedName!~Android)&(FullyQualifiedName!~Samsung)" `
			--logger:"trx;LogFileName=Ocaramba.Tests.saucelabsnet472.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0    
