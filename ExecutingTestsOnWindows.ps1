echo '********************************************Update Chrome********************************************'
#$LocalTempDir = $env:TEMP; $ChromeInstaller = "ChromeInstaller.exe"; 
#(new-object    System.Net.WebClient).DownloadFile('http://dl.google.com/chrome/install/375.126/chrome_installer.exe', "$LocalTempDir\$ChromeInstaller");
#& "$LocalTempDir\$ChromeInstaller" /silent /install; $Process2Monitor =  "ChromeInstaller"; 
#Do { $ProcessesFound = Get-Process | ?{$Process2Monitor -contains $_.Name} | Select-Object -ExpandProperty Name;
#If ($ProcessesFound) { "Still running: $($ProcessesFound -join ', ')" | Write-Host; Start-Sleep -Seconds 2 } 
#else { rm "$LocalTempDir\$ChromeInstaller" -ErrorAction SilentlyContinue -Verbose } } Until (!$ProcessesFound)


$ChromeVersion=(Get-Item (Get-ItemProperty 'HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe' -ErrorAction Stop).'(Default)').VersionInfo.FileVersion;
$TempFilePath = [System.IO.Path]::GetTempFileName();
$TempZipFilePath = $TempFilePath.Replace(".tmp", ".zip");
Rename-Item -Path $TempFilePath -NewName $TempZipFilePath;
$TempFileUnzipPath = $TempFilePath.Replace(".tmp", "");
Invoke-WebRequest "https://chromedriver.storage.googleapis.com/$ChromeVersion/chromedriver_win32.zip" -OutFile $TempZipFilePath;
Expand-Archive $TempZipFilePath -DestinationPath $TempFileUnzipPath;

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Debug\netcoreapp3.1" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Angular\bin\Debug\netcoreapp3.1" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnitExtentReports\bin\Debug\netcoreapp3.1" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Features\bin\Debug\netcoreapp3.1" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Xunit\bin\Debug\netcoreapp3.1" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json 
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.MsTest\bin\Debug\netcoreapp3.1" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.UnitTests\bin\Debug\netcoreapp3.1" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Angular\bin\Debug\net472" "Ocaramba.Tests.Angular.dll.config" "//appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Debug\net472" "Ocaramba.Tests.NUnit.dll.config" "//appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.UnitTests\bin\Debug\net472" "Ocaramba.UnitTests.dll.config" "//appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues


echo '********************************************Executing tests********************************************'
        
echo '********************************************NUnit tests********************************************'

& dotnet.exe test --configuration Debug --filter TestCategory!=TakingScreehShots --no-build --no-restore Ocaramba.Tests.NUnit -maxCpuCount --test-adapter-path:. --logger:Appveyor

& dotnet.exe test --configuration Debug --no-build --no-restore Ocaramba.Tests.Angular -maxCpuCount --test-adapter-path:. --logger:Appveyor
      
& dotnet.exe test --configuration Debug --filter TestCategory!=TakingScreehShots --no-build --no-restore Ocaramba.UnitTests -maxCpuCount --test-adapter-path:. --logger:Appveyor
 
& nunit3-console.exe .\Ocaramba.Tests.Angular\bin\Debug\net472\Ocaramba.Tests.Angular.dll .\Ocaramba.Tests.NUnit\bin\Debug\net472\Ocaramba.Tests.NUnit.dll .\Ocaramba.UnitTests\bin\Debug\net472\Ocaramba.UnitTests.dll

$output = $env:APPVEYOR_BUILD_FOLDER + "\Ocaramba.Tests.NUnit\bin\Debug\net45\"

#.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Debug\net45\" "Ocaramba.Tests.NUnit.dll.config" "//appSettings" "browser|PathToEdgeChromiumDriverDirectory" "EdgeChromium|$output" -logValues

#echo "Downloading edgeChromiumDriver from:" $env:edgeChromiumDriverUrl
        
#$outputZip = $env:APPVEYOR_BUILD_FOLDER + "\Ocaramba.Tests.NUnit\bin\Debug\net45\edgedriver_win64.zip"	
		
#(New-Object System.Net.WebClient).DownloadFile($env:edgeChromiumDriverUrl, $outputZip)

#Expand-Archive -LiteralPath $outputZip -DestinationPath $output  -Force

#& nunit3-console.exe .\Ocaramba.Tests.NUnit\bin\Debug\net45\Ocaramba.Tests.NUnit.dll --where "Category==BasicNUnit"

echo '********************************************MsTest tests********************************************'

& dotnet.exe test --configuration Debug --filter TestCategory!=TakingScreehShots --no-build --no-restore Ocaramba.Tests.MsTest -maxCpuCount --test-adapter-path:. --logger:Appveyor
    
& vstest.console.exe .\Ocaramba.Tests.MsTest\bin\Debug\net472\Ocaramba.Tests.MsTest.dll /Settings:.\Ocaramba.Tests.MsTest\bin\Debug\net472\Runsettings.runsettings
 
echo '********************************************XUnit tests********************************************'   

$xunit = (Resolve-Path "C:\Users\appveyor\.nuget\packages\xunit.runner.console\*\tools\net452\xunit.console.exe").ToString()

& dotnet.exe test --configuration Debug --no-build --no-restore Ocaramba.Tests.Xunit -maxCpuCount --test-adapter-path:. --logger:Appveyor
 
& $xunit .\Ocaramba.Tests.Xunit\bin\Debug\net472\Ocaramba.Tests.Xunit.dll -appveyor -noshadow

echo '********************************************Specflow tests********************************************'   
      
& dotnet.exe test --configuration Debug --filter TestCategory!=TakingScreehShots --no-build --no-restore Ocaramba.Tests.Features -maxCpuCount --test-adapter-path:. --logger:Appveyor


#echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
#echo '********************************************BrowserStack tests********************************************'
        
   
#.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "http://$($env:browserstackuser):$($env:browserstackkey)@hub-cloud.browserstack.com/wd/hub"
        
#.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//DriverCapabilities" "build" "Ocaramba.Tests.BrowserStackCrossBrowser$env:appveyor_build_version" $true

#& nunit3-console.exe .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll --workers=5
    
#.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\netcoreapp3.1" "appsettings.json" "appSettings" "RemoteWebDriverHub" "http://$($env:browserstackuser):$($env:browserstackkey)@hub-cloud.browserstack.com/wd/hub" $false $true
        
#.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\netcoreapp3.1" "appsettings.json" "DriverCapabilities" "build" "Ocaramba.Tests.BrowserStackCrossBrowser$env:appveyor_build_version" $true $true
    
#& dotnet.exe test --configuration Debug --no-build --no-restore Ocaramba.Tests.CloudProviderCrossBrowser -maxCpuCount --test-adapter-path:. --logger:Appveyor
 
#echo '********************************************testingbot.com tests********************************************'  
    
#.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "https://$($env:testingbotkey):$($env:testingbotsecret)@hub.testingbot.com/wd/hub/"
    
#.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//DriverCapabilities" "build" "$Ocaramba.Tests.TestingBotCrossBrowser$env:appveyor_build_version" $true
   
#& nunit3-console.exe .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll --workers=1 --where "test =~ Chrome" --result=myresults.xml`;format=AppVeyor
   
#echo '********************************************saucelabs tests********************************************'
    
#.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "http://$($env:saucelabsusername):$($env:saucelabsaccessKey)@ondemand.saucelabs.com:80/wd/hub"
    
#.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//DriverCapabilities" "build" "Ocaramba.Tests.SauceLabsCrossBrowser$env:appveyor_build_version" $true
    
#.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//environments/SafariMac" "platform" "macOS 10.14" $true
    
#& nunit3-console.exe .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll --workers=5 --where "test =~ Chrome || test =~ IE || test =~ Safari || test =~ Firefox" --result=myresults.xml`;format=AppVeyor
      
#echo '********************************************Downloading Selenium Grid********************************************' 
    
#$url = $env:seleniumGridUrl
        
#$grid = $env:seleniumGridVersion
        
#$output = $env:APPVEYOR_BUILD_FOLDER + "\Ocaramba.Tests.NUnit\bin\Debug\net472\$grid"
        
#$outputLogs = $env:APPVEYOR_BUILD_FOLDER + "\Ocaramba.Tests.NUnit\bin\Debug\net472\"
        
#$start_time = Get-Date

#echo "Downloading Selenium Grid from:" $url
        
#(New-Object System.Net.WebClient).DownloadFile($url, $output)
        
#echo "Selenium Grid downloaded to:" $output
        
#echo "Time taken to download $($grid): $((Get-Date).Subtract($start_time).Seconds) second(s)"
        
#echo '******************************************Start Selenium Grid in background****************************************' 
        
#$appHub=Start-Process java -ArgumentList '-jar', $output' -role hub' -RedirectStandardOutput $outputLogs'console_hub.out' -RedirectStandardError $outputLogs'console_hub.err' -passthru

#Start-Sleep -s 5
        
#echo "Selenium Grid hub started"

#$appNode=Start-Process java -ArgumentList '-jar', $output' -role node  -hub http://localhost:4444/grid/register' -RedirectStandardOutput $outputLogs'console_node.out' -RedirectStandardError $outputLogs'console_node.err' -passthru 
        
#Start-Sleep -s 5
        
#echo "Selenium Grid node started"
        
#echo '********************************************Run tests with Selenium Grid ****************************************' 
        
#.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Debug\net472\" "Ocaramba.Tests.NUnit.dll.config" "//appSettings" "browser|RemoteWebDriverHub" "RemoteWebDriver|http://localhost:4444/wd/hub" $true
        
#& nunit3-console.exe .\Ocaramba.Tests.NUnit\bin\Debug\net472\Ocaramba.Tests.NUnit.dll --where:cat==BasicNUnit --result=myresults.xml`;format=AppVeyor
 
#echo '*****************************Run CloudProviderCrossBrowser tests with Selenium Grid****************************'
        
#.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "http://localhost:4444/wd/hub" $true
        
#& nunit3-console.exe .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Debug\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll --where "test =~ Chrome" --result=myresults.xml`;format=AppVeyor

#if($lastexitcode -ne 0)
# {
#  echo 'lastexitcode' $lastexitcode
# }

#echo '*****************************Stop Selenium Grid****************************'
 
#echo "Stop Selenium Grid node" 
 
#Stop-Process -Id $appNode.Id
        
#echo "Stop Selenium Grid hub" 
        
#Stop-Process -Id $appHub.Id   
        
#echo '*****************************Add Selenium Grid logs to testresults****************************'
        
#7z a testresults_$env:appveyor_build_version.zip .\Ocaramba.*\bin\Debug\**\*.err
        
#7z a testresults_$env:appveyor_build_version.zip .\Ocaramba.*\bin\Debug\**\*.out
       
#7z a testresults_$env:appveyor_build_version.zip .\Ocaramba.*\bin\Debug\**\TestOutput\*.png

#7z a testresults_$env:appveyor_build_version.zip .\Ocaramba.*\bin\Debug\**\TestOutput\*.html

#7z a testresults_$env:appveyor_build_version.zip .\Ocaramba.*\bin\Debug\**\*.log

#appveyor PushArtifact testresults_$env:appveyor_build_version.zip
