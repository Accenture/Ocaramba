echo '********************************************Executing tests********************************************'
        
echo '********************************************NUnit tests********************************************'

$OpenCover = (Resolve-Path "C:\Users\appveyor\.nuget\packages\opencover\*\tools\OpenCover.Console.exe").ToString()
    
& $OpenCover -target:"dotnet.exe" -targetargs:"test --configuration Release --no-build --no-restore Ocaramba.Tests.Angular -maxCpuCount --test-adapter-path:. --logger:Appveyor" -filter:"+[Ocaramba]*" -oldStyle -register:user -output:opencoverCoverage.xml   

& $OpenCover -target:"dotnet.exe" -mergeoutput -targetargs:"test --configuration Release --filter TestCategory!=TakingScreehShots --no-build --no-restore Ocaramba.Tests.NUnit -maxCpuCount --test-adapter-path:. --logger:Appveyor" -filter:"+[Ocaramba]*" -oldStyle -register:user -output:opencoverCoverage.xml
      
& $OpenCover -target:"dotnet.exe" -mergeoutput -targetargs:"test --configuration Release --filter TestCategory!=TakingScreehShots --no-build --no-restore Ocaramba.UnitTests -maxCpuCount --test-adapter-path:. --logger:Appveyor" -filter:"+[Ocaramba]*" -oldStyle -register:user -output:opencoverCoverage.xml
 
& nunit3-console.exe .\Ocaramba.Tests.Angular\bin\Release\net472\Ocaramba.Tests.Angular.dll .\Ocaramba.Tests.NUnit\bin\Release\net472\Ocaramba.Tests.NUnit.dll .\Ocaramba.UnitTests\bin\Release\net472\Ocaramba.UnitTests.dll

echo '********************************************MsTest tests********************************************'

& $OpenCover -target:"dotnet.exe" -mergeoutput -targetargs:"test --configuration Release --filter TestCategory!=TakingScreehShots --no-build --no-restore Ocaramba.Tests.MsTest -maxCpuCount --test-adapter-path:. --logger:Appveyor" -filter:"+[Ocaramba]*" -oldStyle -register:user -output:opencoverCoverage.xml
    
& vstest.console.exe .\Ocaramba.Tests.MsTest\bin\Release\net472\Ocaramba.Tests.MsTest.dll /Settings:.\Ocaramba.Tests.MsTest\bin\Release\net472\Runsettings.runsettings
 
echo '********************************************XUnit tests********************************************'   

$xunit = (Resolve-Path "C:\Users\appveyor\.nuget\packages\xunit.runner.console\*\tools\net452\xunit.console.exe").ToString()

& $OpenCover -target:"dotnet.exe" -mergeoutput -targetargs:"test --configuration Release --no-build --no-restore Ocaramba.Tests.Xunit -maxCpuCount --test-adapter-path:. --logger:Appveyor" -filter:"+[Ocaramba]*" -oldStyle -register:user -output:opencoverCoverage.xml
 
& $xunit .\Ocaramba.Tests.Xunit\bin\Release\net472\Ocaramba.Tests.Xunit.dll -appveyor -noshadow

echo '********************************************Specflow tests********************************************'   
      
& $OpenCover -target:"dotnet.exe" -mergeoutput -targetargs:"test --configuration Release --filter TestCategory!=TakingScreehShots --no-build --no-restore Ocaramba.Tests.Features -maxCpuCount --test-adapter-path:. --logger:Appveyor" -filter:"+[Ocaramba]*" -oldStyle -register:user -output:opencoverCoverage.xml
 
& nunit3-console.exe .\Ocaramba.Tests.Features\bin\Release\net45\Ocaramba.Tests.Features.dll 

echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
        
   
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "http://$($env:browserstackuser):$($env:browserstackkey)@hub-cloud.browserstack.com/wd/hub"
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//DriverCapabilities" "build" "Ocaramba.Tests.BrowserStackCrossBrowser$env:appveyor_build_version" $true

& nunit3-console.exe .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll --workers=5
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\netcoreapp2.2" "appsettings.json" "appSettings" "RemoteWebDriverHub" "http://$($env:browserstackuser):$($env:browserstackkey)@hub-cloud.browserstack.com/wd/hub" $false $true
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\netcoreapp2.2" "appsettings.json" "DriverCapabilities" "build" "Ocaramba.Tests.BrowserStackCrossBrowser$env:appveyor_build_version" $true $true
    
& $OpenCover -target:"dotnet.exe" -mergeoutput -targetargs:"test --configuration Release --no-build --no-restore Ocaramba.Tests.CloudProviderCrossBrowser -maxCpuCount --test-adapter-path:. --logger:Appveyor" -filter:"+[Ocaramba]*" -oldStyle -register:user -output:opencoverCoverage.xml
 
echo '********************************************testingbot.com tests********************************************'  
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "https://$($env:testingbotkey):$($env:testingbotsecret)@hub.testingbot.com/wd/hub/"
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//DriverCapabilities" "build" "$Ocaramba.Tests.TestingBotCrossBrowser$env:appveyor_build_version" $true
   
& nunit3-console.exe .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll --workers=1 --where "test =~ Chrome" --result=myresults.xml`;format=AppVeyor
   
echo '********************************************saucelabs tests********************************************'
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "http://$($env:saucelabsusername):$($env:saucelabsaccessKey)@ondemand.saucelabs.com:80/wd/hub"
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//DriverCapabilities" "build" "Ocaramba.Tests.SauceLabsCrossBrowser$env:appveyor_build_version" $true
    
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//environments/SafariMac" "platform" "macOS 10.14" $true
    
& nunit3-console.exe .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll --workers=5 --where "test =~ Chrome || test =~ IE || test =~ Safari || test =~ Firefox" --result=myresults.xml`;format=AppVeyor
      
echo '********************************************Downloading Selenium Grid********************************************' 
    
$url = $env:seleniumGridUrl
        
$grid = $env:seleniumGridVersion
        
$output = $env:APPVEYOR_BUILD_FOLDER + "\Ocaramba.Tests.NUnit\bin\Release\net472\$grid"
        
$outputLogs = $env:APPVEYOR_BUILD_FOLDER + "\Ocaramba.Tests.NUnit\bin\Release\net472\"
        
$start_time = Get-Date

echo "Downloading Selenium Grid from:" $url
        
(New-Object System.Net.WebClient).DownloadFile($url, $output)
        
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
        
& nunit3-console.exe .\Ocaramba.Tests.NUnit\bin\Release\net472\Ocaramba.Tests.NUnit.dll --where:cat==BasicNUnit --result=myresults.xml`;format=AppVeyor
 
echo '*****************************Run CloudProviderCrossBrowser tests with Selenium Grid****************************'
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472" "Ocaramba.Tests.CloudProviderCrossBrowser.dll.config" "//appSettings" "RemoteWebDriverHub" "http://localhost:4444/wd/hub" $true
        
& nunit3-console.exe .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net472\Ocaramba.Tests.CloudProviderCrossBrowser.dll --where "test =~ Chrome" --result=myresults.xml`;format=AppVeyor

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }

echo '*****************************Stop Selenium Grid****************************'
 
echo "Stop Selenium Grid node" 
 
Stop-Process -Id $appNode.Id
        
echo "Stop Selenium Grid hub" 
        
Stop-Process -Id $appHub.Id
        
echo '*******************************************Sending coverage test results********************************************'
        
& .\packages\csmacnz.Coveralls.exe --opencover -i opencoverCoverage.xml --repoToken $env:COVERALLS_REPO_TOKEN --useRelativePaths --commitId $env:APPVEYOR_REPO_COMMIT --commitBranch $env:APPVEYOR_REPO_BRANCH --commitAuthor $env:APPVEYOR_REPO_COMMIT_AUTHOR --commitEmail $env:APPVEYOR_REPO_COMMIT_AUTHOR_EMAIL --commitMessage $env:APPVEYOR_REPO_COMMIT_MESSAGE --jobId $env:APPVEYOR_BUILD_NUMBER --serviceName appveyor

7z a testresults_$env:appveyor_build_version.zip opencoverCoverage.xml
        
echo '*****************************Add Selenium Grid logs to testresults****************************'
        
7z a testresults_$env:appveyor_build_version.zip .\Ocaramba.*\bin\Release\**\*.err
        
7z a testresults_$env:appveyor_build_version.zip .\Ocaramba.*\bin\Release\**\*.out
       
7z a testresults_$env:appveyor_build_version.zip .\Ocaramba.*\bin\Release\**\TestOutput\*.png

7z a testresults_$env:appveyor_build_version.zip .\Ocaramba.*\bin\Release\**\TestOutput\*.html

7z a testresults_$env:appveyor_build_version.zip .\Ocaramba.*\bin\Release\**\*.log

appveyor PushArtifact testresults_$env:appveyor_build_version.zip