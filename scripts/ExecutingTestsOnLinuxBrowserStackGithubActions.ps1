echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
$Env:ASPNETCORE_ENVIRONMENT="Linux"

echo $Env:ASPNETCORE_ENVIRONMENT

$filePath = ".\Ocaramba.Tests.BrowserStack\bin\Release\net8.0\browserstack.yml"

$fileContent = Get-Content -Path $filePath

$fileContent = $fileContent -replace "BROWSERSTACKUSER", "`$($env:MAPPED_ENV_BROWSERSTACKUSER)"
$fileContent = $fileContent -replace "BROWSERSTACKKEY", "`$($env:MAPPED_ENV_BROWSERSTACKKEY)"

# Write the updated content back to the file
Set-Content -Path $filePath -Value $fileContent

dotnet vstest ./Ocaramba.Tests.BrowserStack/bin/Release/net8.0/Ocaramba.Tests.BrowserStack.dll /Logger:"trx;LogFileName=Ocaramba.Tests.BrowserStacknetcoreapp.xml"

Compress-Archive -Path "./Ocaramba.Tests.BrowserStack/bin/Release/net8.0/TestOutput/*" -DestinationPath "./Ocaramba.Tests.BrowserStack/bin/Release/net8.0/ExecutingTestsOnBrowserStackLinux$env:GITHUB_RUN_ID.zip"
			  
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 } 
exit 0    
