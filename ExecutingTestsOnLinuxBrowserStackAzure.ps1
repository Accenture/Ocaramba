echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
$Env:ASPNETCORE_ENVIRONMENT="Linux"

echo $Env:ASPNETCORE_ENVIRONMENT
echo $env:MAPPED_ENV_BROWSERSTACKUSER
echo $env:MAPPED_ENV_BROWSERSTACKKEY
echo $Env:MAPPED_ENV_BROWSERSTACKUSER
echo $Env:MAPPED_ENV_BROWSERSTACKKEY
echo $Env:MAPPED_ENV_TESTINGBOTKEY
echo $Env:MAPPED_ENV_TESTINGBOTSECRET
echo $Env:MAPPED_ENV_SAUCELABSACCESSKEY
echo $Env:MAPPED_ENV_SAUCELABSUSERNAME

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net6.0" "appsettings.Linux.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$($env:CHROMEWEBDRIVER)" -logValues -json

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net6.0" "appsettings.Linux.json" "appSettings" "RemoteWebDriverHub" "https://$($env:MAPPED_ENV_BROWSERSTACKUSER):$($env:MAPPED_ENV_BROWSERSTACKKEY)@hub-cloud.browserstack.com/wd/hub" -json
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net6.0" "appsettings.Linux.json" "DriverCapabilities" "buildName" "Ocaramba.Tests.BrowserStackCrossBrowser$($env:BuildVersion)" -logValues -json

dotnet vstest ./Ocaramba.Tests.CloudProviderCrossBrowser/bin/Release/net6.0/Ocaramba.Tests.CloudProviderCrossBrowser.dll `
	          --logger:"trx;LogFileName=Ocaramba.Tests.BrowserStacknetcoreapp.xml"
			  
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 } 
exit 0    
