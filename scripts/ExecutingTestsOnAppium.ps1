# Install Node.js dependencies and Appium
echo "Installing Node.js dependencies and Appium..."
npm install -g appium
npm install -g appium-doctor

# Check Appium installation
echo "Checking Appium setup..."
appium-doctor --android

# Start Appium server
echo "Starting Appium server..."
$appiumCommand = "appium --base-path /wd/hub"
$job = Start-Job -ScriptBlock { param($cmd) Invoke-Expression $cmd } -ArgumentList $appiumCommand
Write-Host "Appium server started in background. Job ID: $($job.Id)"

# Configure test environment
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.json" "appSettings" "RemoteWebDriverHub" "http://localhost:4723/wd/hub" -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.CloudProviderCrossBrowser\bin\Release\net8.0" "appsettings.json" "appSettings" "browser" "Android" -json

# Run Android mobile tests
dotnet vstest ./Ocaramba.Tests.CloudProviderCrossBrowser/bin/Release/net8.0/Ocaramba.Tests.CloudProviderCrossBrowser.dll /TestCaseFilter:"FullyQualifiedName~Android" /Logger:"trx;LogFileName=Ocaramba.Tests.AppiumAndroid.xml"

# Stop Appium server
Stop-Job -Job $job
Remove-Job -Job $job
