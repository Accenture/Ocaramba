$vstest = (Resolve-Path "D:\a\_temp\VsTest\Microsoft.TestPlatform*\tools\net*\Common*\IDE\Extensions\TestPlatform\vstest.console.exe").ToString()
 
echo '********************************************Downloading Selenium Grid********************************************' 
    
$url = "https://selenium-release.storage.googleapis.com/4.0-beta-4/selenium-server-4.0.0-beta-4.jar"
        
$grid = "selenium-server-4.0.0-beta-4.jar"
        
$output = $PSScriptRoot + "\Ocaramba.Tests.NUnit\bin\Release\net472\$grid"
        
$outputLogs = $PSScriptRoot + "\Ocaramba.Tests.NUnit\bin\Release\net472\"
        
$start_time = Get-Date

echo url: $url
echo grid: $grid
echo output: $output
echo outputLogs: $outputLogs

echo "Downloading Selenium Grid from: $($url) to $($output)"
Invoke-WebRequest -Uri "$($url)" -Out "$($output)"      
        
echo "Selenium Grid downloaded to:" $output
        
echo "Time taken to download $($grid): $((Get-Date).Subtract($start_time).Seconds) second(s)"
        
echo '******************************************Start Selenium Grid in background****************************************' 
        
$appHub=Start-Process java -ArgumentList '-jar', $output' -role hub' -RedirectStandardOutput $outputLogs'console_hub.out' -RedirectStandardError $outputLogs'console_hub.err' -passthru

Start-Sleep -s 5
        
echo "Selenium Grid hub started"

$appNode=Start-Process java -ArgumentList '-jar', $output' -role node  -hub http://localhost:4444/grid/register' -RedirectStandardOutput $outputLogs'console_node.out' -RedirectStandardError $outputLogs'console_node.err' -passthru 
        
Start-Sleep -s 5
        
echo "Selenium Grid node started"
        
echo '********************************************Run tests with Selenium Grid ****************************************' 
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\net472\" "Ocaramba.Tests.NUnit.dll.config" "//appSettings" "browser|RemoteWebDriverHub" "RemoteWebDriver|http://localhost:4444/wd/hub" $true
        
& $vstest .\Ocaramba.Tests.NUnit\bin\Release\net472\Ocaramba.Tests.NUnit.dll /TestCaseFilter:"TestCategory=BasicNUnit" /Parallel `
			--logger:"trx;LogFileName=Ocaramba.Tests.NUnitGrid.xml"
 
echo '*****************************Run CloudProviderCrossBrowser tests with Selenium Grid****************************'
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "http://localhost:4444/wd/hub" $true
        
& $vstest .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll /TestCaseFilter:"FullyQualifiedName~Chrome" /Parallel `
			--logger:"trx;LogFileName=Ocaramba.Tests.CloudProviderCrossBrowserGrid.xml"
			
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }

echo '*****************************Stop Selenium Grid****************************'
 
echo "Stop Selenium Grid node" 
 
Stop-Process -Id $appNode.Id
        
echo "Stop Selenium Grid hub" 
        
Stop-Process -Id $appHub.Id   

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0    
