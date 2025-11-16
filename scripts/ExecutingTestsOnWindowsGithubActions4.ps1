echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
cd Ocaramba.Tests.BrowserStack/bin/Release/net8.0/

dotnet browserstack-sdk Ocaramba.Tests.BrowserStack.dll /Logger:"trx;LogFileName=Ocaramba.Tests.BrowserStack.xml"

Copy-Item -Path "*.log" -Destination "./TestOutput/" -Recurse
Compress-Archive -Path "./TestOutput/*" -DestinationPath "./ExecutingTestsOnBrowserStackWindows$env:GITHUB_RUN_ID.zip"
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }


exit 0    


