echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
$Env:ASPNETCORE_ENVIRONMENT="Linux"

echo $Env:ASPNETCORE_ENVIRONMENT

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\netcoreapp3.1" "appsettings.json" "appSettings" "RemoteWebDriverHub" "https://$($env:MAPPED_ENV_BROWSERSTACKUSER):$($env:MAPPED_ENV_BROWSERSTACKKEY)@hub-cloud.browserstack.com/wd/hub" -json
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\netcoreapp3.1" "appsettings.json" "DriverCapabilities" "buildName" "Ocaramba.Tests.BrowserStackCrossBrowser$($env:BuildVersion)" -logValues -json

dotnet vstest ./Ocaramba.Tests.CloudProviderCrossBrowser/bin/Release/netcoreapp3.1/Ocaramba.Tests.CloudProviderCrossBrowser.dll `
	          --logger:"trx;LogFileName=Ocaramba.Tests.BrowserStacknetcoreapp.xml"
			  
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 } 
exit 0    
