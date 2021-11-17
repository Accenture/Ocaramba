echo '********************************************Executing tests********************************************'
        
echo '********************************************netcoreapp3 tests********************************************'

dotnet vstest .\Ocaramba.Tests.Features\bin\Release\netcoreapp3.1\Ocaramba.Tests.Features.dll `
			  .\Ocaramba.Tests.Xunit\bin\Release\netcoreapp3.1\Ocaramba.Tests.Xunit.dll `
			  .\Ocaramba.Tests.MsTest\bin\Release\netcoreapp3.1\Ocaramba.Tests.MsTest.dll `
	          .\Ocaramba.UnitTests\bin\Release\netcoreapp3.1\Ocaramba.UnitTests.dll `
			  /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel `
	          --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"


echo '********************************************EdgeChrominum tests********************************************'

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\netcoreapp3.1" "appsettings.json" "appSettings" "browser|PathToEdgeChromiumDriverDirectory" "EdgeChromium|$($env:EDGEWEBDRIVER)" $true $true
        
$outputZip = $PSScriptRoot + "\Ocaramba.Tests.NUnit\bin\Release\netcoreapp3.1\edgedriver_win64.zip"	
echo outputZip: $outputZip
Invoke-WebRequest -Uri "$($url)" -Out "$($output)"  		

Expand-Archive -LiteralPath $outputZip -DestinationPath $outputPath  -Force

dotnet vstest .\Ocaramba.Tests.NUnit\bin\Release\netcoreapp3.1\Ocaramba.Tests.NUnit.dll `
			  /TestCaseFilter:"(TestCategory=Grid)" /Parallel `
	          --logger:"trx;LogFileName=Ocaramba.Tests.EdgeChrominum.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0