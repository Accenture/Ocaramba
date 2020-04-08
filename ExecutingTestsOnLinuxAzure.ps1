echo '********************************************Executing tests********************************************'
        
echo '********************************************netcoreapp3 tests********************************************'

dotnet vstest ./Ocaramba.Tests.Angular/bin/Release/netcoreapp3.1/Ocaramba.Tests.Angular.dll `
	          ./Ocaramba.Tests.NUnit/bin/Release/netcoreapp3.1/Ocaramba.Tests.NUnit.dll `
			  ./Ocaramba.Tests.Xunit/bin/Release/netcoreapp3.1/Ocaramba.Tests.Xunit.dll `
			  ./Ocaramba.Tests.MsTest/bin/Release/netcoreapp3.1/Ocaramba.Tests.MsTest.dll `
	          ./Ocaramba.UnitTests/bin/Release/netcoreapp3.1/Ocaramba.UnitTests.dll `
			  /TestCaseFilter:"(TestCategory!=TakingScreehShots)" `
	          --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0