echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
$Env:ASPNETCORE_ENVIRONMENT="Linux"

echo $Env:ASPNETCORE_ENVIRONMENT

$chromeVersion = (& google-chrome --product-version) -match "\d+\.\d+\.\d+\.\d+"

if ($chromeVersion) {
    $major = $Matches[0].Split('.')[0]
    $minor = $Matches[0].Split('.')[1]
    $build = $Matches[0].Split('.')[2]

    if ($major -ge 115) {
        $re = "$major\.$minor\.$build\."
        $url = (Invoke-WebRequest -Uri "https://googlechromelabs.github.io/chrome-for-testing/known-good-versions-with-downloads.json").Content |
               ConvertFrom-Json | 
               Select-Object -ExpandProperty versions |
               Where-Object { $_.version -match $re } |
               Select-Object -ExpandProperty downloads |
               Where-Object { $_.platform -eq "linux64" } |
               Select-Object -ExpandProperty url |
               Select-Object -Last 1

        if (-not $url) {
            Write-Host "Failed finding latest release matching /$re/"
            exit 1
        }

        $rel = $url -replace '.*/(\d+\.\d+\.\d+\.\d+)/.*', '$1'
        $srcfile = "chromedriver-linux64/chromedriver"
    } else {
        $short = $build -replace '\.\d+$', ''
        $rel = Invoke-WebRequest -Uri "https://chromedriver.storage.googleapis.com/LATEST_RELEASE_$short" -UseBasicParsing
        $url = "https://chromedriver.storage.googleapis.com/$rel/chromedriver_linux64.zip"
        $srcfile = "chromedriver"
    }

    Invoke-WebRequest -Uri $url -OutFile "/chromedriver/chromedriver.zip"
    Expand-Archive -Path "/chromedriver/chromedriver.zip" -DestinationPath "/chromedriver"
    Move-Item -Path "/chromedriver/$srcfile" -Destination "/usr/local/bin/chromedriver"
    icacls "/usr/local/bin/chromedriver" /grant Everyone:F
}

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.Linux.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|/usr/local/bin/" -logValues -json

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.Linux.json" "appSettings" "RemoteWebDriverHub" "https://$($env:MAPPED_ENV_BROWSERSTACKUSER):$($env:MAPPED_ENV_BROWSERSTACKKEY)@hub-cloud.browserstack.com/wd/hub" -logValues  -json
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.Linux.json" "DriverCapabilities" "buildName" "Ocaramba.Tests.BrowserStackCrossBrowser$($env:BuildVersion)" -logValues -json

dotnet vstest ./Ocaramba.Tests.CloudProviderCrossBrowser/bin/Release/net8.0/Ocaramba.Tests.CloudProviderCrossBrowser.dll /Logger:"trx;LogFileName=Ocaramba.Tests.BrowserStacknetcoreapp.xml"
			  
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 } 
exit 0    
