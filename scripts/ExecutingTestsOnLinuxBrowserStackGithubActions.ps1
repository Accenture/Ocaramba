Write-Output '********************************************CloudProviderCrossBrowser tests********************************************'
        
Write-Output '********************************************BrowserStack tests********************************************'
$Env:ASPNETCORE_ENVIRONMENT="Linux"

Write-Output $Env:ASPNETCORE_ENVIRONMENT

dotnet browserstack-sdk --no-build  ./Ocaramba.Tests.BrowserStack/bin/Release/net8.0/Ocaramba.Tests.BrowserStack.dll /Logger:"trx;LogFileName=Ocaramba.Tests.BrowserStacknetcoreapp.xml"

Compress-Archive -Path "./Ocaramba.Tests.BrowserStack/bin/Release/net8.0/TestOutput/*" -DestinationPath "./Ocaramba.Tests.BrowserStack/bin/Release/net8.0/ExecutingTestsOnBrowserStackLinux$env:GITHUB_RUN_ID.zip"
			  
if($lastexitcode -ne 0)
 {
  Write-Output 'lastexitcode' $lastexitcode
 } 
exit 0    
