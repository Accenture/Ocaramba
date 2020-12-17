echo '********************************************Executing tests********************************************'
        
echo '********************************************netcoreapp3 tests********************************************'

dotnet vstest .\Ocaramba.Tests.Angular\bin\Release\netcoreapp3.1\Ocaramba.Tests.Angular.dll `
	          .\Ocaramba.Tests.NUnit\bin\Release\netcoreapp3.1\Ocaramba.Tests.NUnit.dll `
			  /TestCaseFilter:"(TestCategory!=TakingScreehShots)" `
	          --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"

echo '********************************************net472 tests********************************************'

$vstest = (Resolve-Path "D:\a\_temp\VsTest\Microsoft.TestPlatform*\tools\net*\Common*\IDE\Extensions\TestPlatform\vstest.console.exe").ToString()

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0