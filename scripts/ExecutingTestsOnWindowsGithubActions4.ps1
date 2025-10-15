echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
cd Ocaramba.Tests.BrowserStack/bin/Release/net8.0/

dotnet browserstack-sdk --no-build Ocaramba.Tests.BrowserStack.dll /Logger:"trx;LogFileName=Ocaramba.Tests.BrowserStack.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }

cd ./../../../..

$dest = (Resolve-Path './Ocaramba.Tests.BrowserStack/bin/Release/net8.0/TestOutput').Path
New-Item -ItemType Directory -Path $dest -Force | Out-Null

Get-ChildItem -Path . -Recurse -File -Filter '*.log' |
  Where-Object {
    $_.FullName -notlike "$dest*"
  } |
  Copy-Item -Destination $dest -Force

Compress-Archive -Path "./Ocaramba.Tests.BrowserStack/bin/Release/net8.0/TestOutput/*" -DestinationPath "ExecutingTestsOnWindowsBrowserStack$env:GITHUB_RUN_ID.zip"

exit 0    
