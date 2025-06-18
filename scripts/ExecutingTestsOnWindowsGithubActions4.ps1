echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
$filePath = ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0\browserstack.yml"

$fileContent = Get-Content -Path $filePath

$fileContent = $fileContent -replace "BROWSERSTACKUSER", "`$($env:MAPPED_ENV_BROWSERSTACKUSER)"
$fileContent = $fileContent -replace "BROWSERSTACKKEY", "`$($env:MAPPED_ENV_BROWSERSTACKKEY)"

# Write the updated content back to the file
dotnet test --logger:"nunit;LogFilePath=./TestResults/Ocaramba.Tests.BrowserStack.xml" --results-directory ./TestResults
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }

exit 0    
