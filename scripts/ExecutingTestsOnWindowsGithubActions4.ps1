echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
cd Ocaramba.Tests.BrowserStack/bin/Release/net8.0/

dotnet browserstack-sdk --no-build Ocaramba.Tests.BrowserStack.dll /Logger:"trx;LogFileName=Ocaramba.Tests.BrowserStack.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }

Copy-Item -Path "*.log" -Destination "./TestOutput/" -Recurse
Compress-Archive -Path "./TestOutput/*" -DestinationPath "ExecutingTestsOnWindowsBrowserStack$env:GITHUB_RUN_ID.zip"

exit 0    
