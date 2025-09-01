echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
$filePath = ".\Ocaramba.Tests.BrowserStack\bin\Release\net8.0\browserstack.yml"
$fileContent = Get-Content -Path $filePath

$fileContent = $fileContent -replace "BROWSERSTACK_USERNAME", "`$($env:MAPPED_ENV_BROWSERSTACKUSER)"
$fileContent = $fileContent -replace "BROWSERSTACK_ACCESS_KEY", "`$($env:MAPPED_ENV_BROWSERSTACKKEY)"

# Write the updated content back to the file
Set-Content -Path $filePath -Value $fileContent

dotnet vstest ./Ocaramba.Tests.BrowserStack/bin/Release/net8.0/Ocaramba.Tests.BrowserStack.dll /Logger:"trx;LogFileName=Ocaramba.Tests.BrowserStack.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
Compress-Archive -Path "./Ocaramba.Tests.BrowserStack/bin/Release/net8.0/TestOutput/*" -DestinationPath "./Ocaramba.Tests.BrowserStack/bin/Release/net8.0/ExecutingTestsOnWindowsBrowserStack$env:GITHUB_RUN_ID.zip"

exit 0    
