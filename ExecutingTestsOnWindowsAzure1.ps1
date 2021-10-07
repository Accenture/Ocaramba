echo '********************************************Update Chrome********************************************'
$LocalTempDir = $env:TEMP; $ChromeInstaller = "ChromeInstaller.exe"; 
(new-object    System.Net.WebClient).DownloadFile('http://dl.google.com/chrome/install/375.126/chrome_installer.exe', "$LocalTempDir\$ChromeInstaller");
& "$LocalTempDir\$ChromeInstaller" /silent /install; $Process2Monitor =  "ChromeInstaller"; 
Do { $ProcessesFound = Get-Process | ?{$Process2Monitor -contains $_.Name} | Select-Object -ExpandProperty Name;
If ($ProcessesFound) { "Still running: $($ProcessesFound -join ', ')" | Write-Host; Start-Sleep -Seconds 2 } 
else { rm "$LocalTempDir\$ChromeInstaller" -ErrorAction SilentlyContinue -Verbose } } Until (!$ProcessesFound)
((Get-Item "C:\Program Files\Google\Chrome\Application\chrome.exe").VersionInfo)
echo '********************************************Executing tests********************************************'
        
echo '********************************************netcoreapp3 tests********************************************'

dotnet vstest .\Ocaramba.Tests.Angular\bin\Release\netcoreapp3.1\Ocaramba.Tests.Angular.dll `
	          .\Ocaramba.Tests.NUnit\bin\Release\netcoreapp3.1\Ocaramba.Tests.NUnit.dll `
			  .\Ocaramba.Tests.NUnitExtentReports\bin\Release\netcoreapp3.1\Ocaramba.Tests.NUnitExtentReports.dll `
			  /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel `
	          --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.trx"

echo '********************************************net472 tests********************************************'

$vstest = (Resolve-Path "D:\a\_temp\VsTest\Microsoft.TestPlatform*\tools\net*\Common*\IDE\Extensions\TestPlatform\vstest.console.exe").ToString()
& $vstest .\Ocaramba.Tests.Features\bin\Release\net472\Ocaramba.Tests.Features.dll .\Ocaramba.Tests.NUnit\bin\Release\net45\Ocaramba.Tests.NUnit.dll .\Ocaramba.Tests.NUnitExtentReports\bin\Release\netcoreapp3.1\Ocaramba.Tests.NUnitExtentReports.dll --logger:"trx;LogFileName=Ocaramba.Tests.Features.trx"
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
