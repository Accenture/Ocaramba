echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "http://$($env:MAPPED_ENV_BROWSERSTACKUSER):$($env:MAPPED_ENV_BROWSERSTACKKEY)@hub-cloud.browserstack.com/wd/hub"
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//DriverCapabilities" "build" "Ocaramba.Tests.BrowserStackCrossBrowser$env:Build.BuildId" $true
    
$vstest = (Resolve-Path "D:\a\_temp\VsTest\Microsoft.TestPlatform*\tools\net*\Common*\IDE\Extensions\TestPlatform\vstest.console.exe").ToString()

& $vstest .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll `
			--logger:"trx;LogFileName=Ocaramba.Tests.BrowserStack.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }

    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\netcoreapp3.1" "appsettings.json" "appSettings" "RemoteWebDriverHub" "http://$($env:MAPPED_ENV_BROWSERSTACKUSER):$($env:MAPPED_ENV_BROWSERSTACKKEY)@hub-cloud.browserstack.com/wd/hub" $false $true
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\netcoreapp3.1" "appsettings.json" "DriverCapabilities" "build" "Ocaramba.Tests.BrowserStackCrossBrowser$env:Build.BuildId" $true $true

dotnet vstest .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\netcoreapp3.1\Ocaramba.Tests.CloudProviderCrossBrowser.dll `
			  /TestCaseFilter:"(TestCategory!=TakingScreehShots)" `
	          --logger:"trx;LogFileName=Ocaramba.Tests.BrowserStacknetcoreapp.xml"
			  
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 } 
 
echo '********************************************testingbot.com tests********************************************'  
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "https://$($env:MAPPED_ENV_TESTINGBOTKEY):$($env:MAPPED_ENV_TESTINGBOTSECRET)@hub.testingbot.com/wd/hub/"
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//DriverCapabilities" "build" "$Ocaramba.Tests.TestingBotCrossBrowser$env:Build.BuildId" $true
   
& $vstest  .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll `
			--logger:"trx;LogFileName=Ocaramba.Tests.testingbot.xml"
			
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
echo '********************************************saucelabs tests********************************************'
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "http://$($env:MAPPED_ENV_SAUCELABSUSERNAME):$($env:MAPPED_ENV_SAUCELABSACCESSKEY)@ondemand.saucelabs.com:80/wd/hub"
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//DriverCapabilities" "build" "Ocaramba.Tests.SauceLabsCrossBrowser$env:Build.BuildId" $true
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//environments/SafariMac" "platform" "macOS 10.14" $true
    
& $vstest .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll 
			--logger:"trx;LogFileName=Ocaramba.Tests.saucelabsnet472.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0    
