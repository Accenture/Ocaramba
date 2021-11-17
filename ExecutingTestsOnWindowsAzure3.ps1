echo '********************************************Executing tests********************************************'
        
echo '********************************************net472 tests*********************************************'
$vstest = (Resolve-Path "D:\a\_temp\VsTest\Microsoft.TestPlatform*\tools\net*\Common*\IDE\Extensions\TestPlatform\vstest.console.exe").ToString()

& $vstest .\Ocaramba.Tests.Angular\bin\Release\net472\Ocaramba.Tests.Angular.dll `
			.\Ocaramba.Tests.NUnit\bin\Release\net472\Ocaramba.Tests.NUnit.dll `
			.\Ocaramba.UnitTests\bin\Release\net472\Ocaramba.UnitTests.dll /Parallel `
			--logger:"trx;LogFileName=Ocaramba.Tests.net4.xml"


if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
