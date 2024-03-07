echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
echo $($env:MAPPED_ENV_BROWSERSTACKUSER)
echo $($env:MAPPED_ENV_BROWSERSTACKKEY)
echo $env:MAPPED_ENV_BROWSERSTACKUSER
echo $env:MAPPED_ENV_BROWSERSTACKKEY


$vstest = (Resolve-Path "D:\a\_temp\VsTest\Microsoft.TestPlatform*\tools\net*\Common*\IDE\Extensions\TestPlatform\vstest.console.exe").ToString()
       
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "https://$($env:MAPPED_ENV_BROWSERSTACKUSER):$($env:MAPPED_ENV_BROWSERSTACKKEY)@hub-cloud.browserstack.com/wd/hub"
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//DriverCapabilities" "buildName" "Ocaramba.Tests.BrowserStackCrossBrowser$($env:BuildVersion)" -logValues 

& $vstest .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll `
			--logger:"trx;LogFileName=Ocaramba.Tests.BrowserStack.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }

exit 0    
