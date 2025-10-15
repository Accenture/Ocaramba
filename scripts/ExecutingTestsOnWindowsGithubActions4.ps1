echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
cd Ocaramba.Tests.BrowserStack/bin/Release/net8.0/

dotnet browserstack-sdk --no-build Ocaramba.Tests.BrowserStack.dll /Logger:"trx;LogFileName=Ocaramba.Tests.BrowserStack.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }

cd ./../../../..
Get-ChildItem -Path "./" -Recurse -Filter *.log -File | Copy-Item -Destination "./Ocaramba.Tests.BrowserStack/bin/Release/net8.0/TestOutput" -Force

Compress-Archive -Path "./Ocaramba.Tests.BrowserStack/bin/Release/net8.0/TestOutput/*" -DestinationPath "ExecutingTestsOnWindowsBrowserStack$env:GITHUB_RUN_ID.zip"

exit 0    
