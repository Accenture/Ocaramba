echo '********************************************Update Chrome********************************************'
$LocalTempDir = $env:TEMP; $ChromeInstaller = "ChromeStandaloneSetup64.exe"; 
(new-object    System.Net.WebClient).DownloadFile('https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7B16597FD3-D497-42D5-1E46-04BAE17B6B4E%7D%26lang%3Den%26browser%3D5%26usagestats%3D0%26appname%3DGoogle%2520Chrome%26needsadmin%3Dprefers%26ap%3Dx64-stable-statsdef_1%26installdataindex%3Dempty/chrome/install/ChromeStandaloneSetup64.exe', "$LocalTempDir\ChromeStandaloneSetup64.exe");
& "$LocalTempDir\$ChromeInstaller" /silent /install; $Process2Monitor =  "ChromeInstaller"; 
Do { $ProcessesFound = Get-Process | ?{$Process2Monitor -contains $_.Name} | Select-Object -ExpandProperty Name;
If ($ProcessesFound) { "Still running: $($ProcessesFound -join ', ')" | Write-Host; Start-Sleep -Seconds 2 } 
else { rm "$LocalTempDir\$ChromeInstaller" -ErrorAction SilentlyContinue -Verbose } } Until (!$ProcessesFound)
((Get-Item "C:\Program Files\Google\Chrome\Application\chrome.exe").VersionInfo)

echo '********************************************Executing tests********************************************'
        
echo '********************************************net472 tests*********************************************'
$vstest = (Resolve-Path "D:\a\_temp\VsTest\Microsoft.TestPlatform*\tools\net*\Common*\IDE\Extensions\TestPlatform\vstest.console.exe").ToString()

& $vstest .\Ocaramba.Tests.Angular\bin\Release\net472\Ocaramba.Tests.Angular.dll `
			.\Ocaramba.Tests.NUnit\bin\Release\net472\Ocaramba.Tests.NUnit.dll `
			.\Ocaramba.UnitTests\bin\Release\net472\Ocaramba.UnitTests.dll `
			--logger:"trx;LogFileName=Ocaramba.Tests.net4.xml"


if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
