echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
       
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "https://$($env:MAPPED_ENV_BROWSERSTACKUSER):$($env:MAPPED_ENV_BROWSERSTACKKEY)@hub-cloud.browserstack.com/wd/hub"
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//DriverCapabilities" "buildName" "Ocaramba.Tests.BrowserStackCrossBrowser$($env:BuildVersion)" -logValues 

dotnet vstest D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll /Logger:"trx;LogFileName=Ocaramba.Tests.BrowserStack.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }

exit 0    
