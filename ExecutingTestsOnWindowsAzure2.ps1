echo '********************************************Executing tests********************************************'
        
echo '********************************************netcoreapp3 tests********************************************'

dotnet vstest .\Ocaramba.Tests.Features\bin\Release\netcoreapp3.1\Ocaramba.Tests.Features.dll `
			  .\Ocaramba.Tests.Xunit\bin\Release\netcoreapp3.1\Ocaramba.Tests.Xunit.dll `
			  .\Ocaramba.Tests.MsTest\bin\Release\netcoreapp3.1\Ocaramba.Tests.MsTest.dll `
	          .\Ocaramba.UnitTests\bin\Release\netcoreapp3.1\Ocaramba.UnitTests.dll `
			  /TestCaseFilter:"(TestCategory!=TakingScreehShots)" `
	          --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"

echo '********************************************net472 tests********************************************'

$vstest = (Resolve-Path "D:\a\_temp\VsTest\Microsoft.TestPlatform*\tools\net*\Common*\IDE\Extensions\TestPlatform\vstest.console.exe").ToString()

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0