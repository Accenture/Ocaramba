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

dotnet vstest .\Ocaramba.Tests.Features\bin\Release\netcoreapp3.1\Ocaramba.Tests.Features.dll `
			  .\Ocaramba.Tests.Xunit\bin\Release\netcoreapp3.1\Ocaramba.Tests.Xunit.dll `
			  .\Ocaramba.Tests.MsTest\bin\Release\netcoreapp3.1\Ocaramba.Tests.MsTest.dll `
	          .\Ocaramba.UnitTests\bin\Release\netcoreapp3.1\Ocaramba.UnitTests.dll `
			  /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel `
	          --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"


echo '********************************************EdgeChrominum tests********************************************'

$url = $env:edgeChromiumDriverUrl
echo url: $url
$outputZip = "edgedriver_win64.zip"	
$output = $PSScriptRoot + "\Ocaramba.Tests.NUnit\bin\Release\netcoreapp3.1\$outputZip"
$outputPath = $PSScriptRoot + "\Ocaramba.Tests.NUnit\bin\Release\netcoreapp3.1"
echo output: $output
echo outputPath: $outputPath


echo "Downloading EdgeChrominum driver from: $($url) to $($output)"
Invoke-WebRequest -Uri "$($url)" -Out "$($output)"  
Expand-Archive -LiteralPath $outputZip -DestinationPath $outputPath -Force
$driver=$outputPath+"msedgedriver.exe"

echo driver: $driver

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Release\netcoreapp3.1\" "appsettings.json" "appSettings" "browser|PathToEdgeChrominumDriverDirectory" "EdgeChromium|$driver" $true $true

echo "Downloading edgeChromiumDriver from:" $env:edgeChromiumDriverUrl
        
$outputZip = $env:APPVEYOR_BUILD_FOLDER + "\Ocaramba.Tests.NUnit\bin\Debug\net45\edgedriver_win64.zip"	
		
(New-Object System.Net.WebClient).DownloadFile($env:edgeChromiumDriverUrl, $outputZip)

Expand-Archive -LiteralPath $outputZip -DestinationPath $output  -Force

dotnet vstest .\Ocaramba.Tests.NUnit\bin\Release\netcoreapp3.1\Ocaramba.Tests.NUnit.dll `
			  /TestCaseFilter:"(TestCategory=BasicNUnit)" /Parallel `
	          --logger:"trx;LogFileName=Ocaramba.Tests.EdgeChrominum.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0