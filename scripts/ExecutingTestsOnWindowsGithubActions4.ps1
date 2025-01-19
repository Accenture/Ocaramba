echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
$filePath = ".\Ocaramba\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0\browserstack.yml"

$fileContent = Get-Content -Path $filePath

$fileContent = $fileContent -replace "BROWSERSTACKUSER", "`$($env:MAPPED_ENV_BROWSERSTACKUSER)"
$fileContent = $fileContent -replace "BROWSERSTACKKEY", "`$($env:MAPPED_ENV_BROWSERSTACKKEY)"

# Write the updated content back to the file
Set-Content -Path $filePath -Value $fileContent       
.\Ocaramba\set_AppConfig_for_tests.ps1 ".\Ocaramba\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.json" "appSettings" "RemoteWebDriverHub" "https://$($env:MAPPED_ENV_BROWSERSTACKUSER):$($env:MAPPED_ENV_BROWSERSTACKKEY)@hub-cloud.browserstack.com/wd/hub" -json
.\Ocaramba\set_AppConfig_for_tests.ps1 ".\Ocaramba\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.json" "DriverCapabilities" "buildName" "Ocaramba.Tests.BrowserStackCrossBrowser$($env:BuildVersion)" -logValues -json

dotnet vstest D:\a\Ocaramba\Ocaramba\Ocaramba\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0\Ocaramba.Tests.CloudProviderCrossBrowser.dll /Logger:"trx;LogFileName=Ocaramba.Tests.BrowserStack.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }

exit 0    
