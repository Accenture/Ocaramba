echo '********************************************Update Chrome********************************************'
$LocalTempDir = ".\"; $ChromeInstaller = "ChromeInstaller.msi"; 
(new-object    System.Net.WebClient).DownloadFile('https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7B16597FD3-D497-42D5-1E46-04BAE17B6B4E%7D%26lang%3Den%26browser%3D5%26usagestats%3D0%26appname%3DGoogle%2520Chrome%26needsadmin%3Dprefers%26ap%3Dx64-stable-statsdef_1%26installdataindex%3Dempty/chrome/install/ChromeStandaloneSetup64.exe', "$LocalTempDir\$ChromeInstaller");

$list = 
@(
    "/I `"$LocalTempDir\$ChromeInstaller`"",                     # Install this MSI
    "/QN",                             # Quietly, without a UI
    "/L*V `".\ChromeIns.log`""     # Verbose output to this log
)
$p = Start-Process -FilePath "msiexec" -ArgumentList $list -Wait 

rm "$LocalTempDir\$ChromeInstaller" -ErrorAction SilentlyContinue -Verbose

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