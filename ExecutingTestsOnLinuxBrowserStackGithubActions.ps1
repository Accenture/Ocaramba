echo '********************************************CloudProviderCrossBrowser tests********************************************'
        
echo '********************************************BrowserStack tests********************************************'
$Env:ASPNETCORE_ENVIRONMENT="Linux"

echo $Env:ASPNETCORE_ENVIRONMENT

CHROMEVER=$(google-chrome --product-version | grep -oE "[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+") \
    && major=$(echo "$CHROMEVER" | cut -d. -f 1) \
    && minor=$(echo "$CHROMEVER" | cut -d. -f 2) \
    && build=$(echo "$CHROMEVER" | cut -d. -f 3) \
    && if [ "$major" -ge 115 ]; then \
        re=^${major}\.${minor}\.${build}\.; \
        url=$(wget --quiet -O- https://googlechromelabs.github.io/chrome-for-testing/known-good-versions-with-downloads.json | \
              jq -r '.versions[] | select(.version | test("'"${re}"'")) | .downloads.chromedriver[] | select(.platform == "linux64") | .url' | \
              tail -1); \
        if [ -z "$url" ]; then \
            echo "Failed finding latest release matching /${re}/"; \
            exit 1; \
        fi; \
        rel=$(echo "$url" | sed -nre 's!.*/([0-9]+\.[0-9]+\.[0-9]+\.[0-9]+)/.*!\1!p'); \
        srcfile=chromedriver-linux64/chromedriver; \
    else \
        short=$(echo "$build" | sed -re 's/\.[0-9]+$//'); \
        rel=$(wget --quiet -O- "https://chromedriver.storage.googleapis.com/LATEST_RELEASE_${short}"); \
        url=https://chromedriver.storage.googleapis.com/${rel}/chromedriver_linux64.zip; \
        srcfile=chromedriver; \
    fi \
    && wget -q --continue -P /chromedriver "$url" \
    && unzip /chromedriver/chromedriver* -d /chromedriver \
    && mv /chromedriver/$srcfile /usr/local/bin/chromedriver \
    && chmod +x /usr/local/bin/chromedriver

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.Linux.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|/usr/local/bin/" -logValues -json

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.Linux.json" "appSettings" "RemoteWebDriverHub" "https://$($env:MAPPED_ENV_BROWSERSTACKUSER):$($env:MAPPED_ENV_BROWSERSTACKKEY)@hub-cloud.browserstack.com/wd/hub" -logValues  -json
        
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.Linux.json" "DriverCapabilities" "buildName" "Ocaramba.Tests.BrowserStackCrossBrowser$($env:BuildVersion)" -logValues -json

dotnet vstest ./Ocaramba.Tests.CloudProviderCrossBrowser/bin/Release/net8.0/Ocaramba.Tests.CloudProviderCrossBrowser.dll /Logger:"trx;LogFileName=Ocaramba.Tests.BrowserStacknetcoreapp.xml"
			  
if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 } 
exit 0    
