Write-Output '********************************************CloudProviderCrossBrowser tests********************************************'
        
Write-Output '********************************************BrowserStack tests********************************************'
$Env:ASPNETCORE_ENVIRONMENT="Linux"

Write-Output $Env:ASPNETCORE_ENVIRONMENT
cd Ocaramba.Tests.BrowserStack/bin/Release/net8.0/
dotnet browserstack-sdk --no-build  Ocaramba.Tests.BrowserStack.dll /Logger:"trx;LogFileName=Ocaramba.Tests.BrowserStacknetcoreapp.xml"

Copy-Item -Path "*.log" -Destination "./TestOutput/" -Recurse
Compress-Archive -Path "./TestOutput/*" -DestinationPath "./ExecutingTestsOnBrowserStackLinux$env:GITHUB_RUN_ID.zip"
			  
if($lastexitcode -ne 0)
 {
  Write-Output 'lastexitcode' $lastexitcode
 } 
exit 0    
