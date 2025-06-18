echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
$filePath = ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0\browserstack.yml"

$fileContent = Get-Content -Path $filePath

$fileContent = $fileContent -replace "BROWSERSTACKUSER", "`$($env:MAPPED_ENV_BROWSERSTACKUSER)"
$fileContent = $fileContent -replace "BROWSERSTACKKEY", "`$($env:MAPPED_ENV_BROWSERSTACKKEY)"

# Write the updated content back to the file
dotnet test .\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0\Ocaramba.Tests.CloudProviderCrossBrowser.dll /Logger:"nunit;LogFileName=Ocaramba.Tests.BrowserStack.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }

exit 0    
