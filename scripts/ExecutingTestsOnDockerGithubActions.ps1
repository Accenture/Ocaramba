docker info
         
docker ps -a
docker exec -u ocaramba ocaramba_selenium bash -c "ls ; sed -i '/Documentation/,+5 d' ./Ocaramba.sln ; sed -i '/CloudProviderCrossBrowser/,+1 d' ./Ocaramba.sln; sed -i '/UnitTests/,+1 d' ./Ocaramba.sln; sed -i '/BrowserStack/,+1 d' ./Ocaramba.sln; sed -i '/MsTest/,+1 d' ./Ocaramba.sln;sed -i '/Features/,+1 d' ./Ocaramba.sln;sed -i '/Xunit/,+1 d' ./Ocaramba.sln;sed -i '/NUnitExtentReports/,+1 d' ./Ocaramba.sln;sed -i '/Appium/,+1 d' ./Ocaramba.sln;sed -i '/Angular/,+1 d' ./Ocaramba.sln;dotnet build ./Ocaramba.sln --configuration $env:buildConfiguration"
docker exec -u ocaramba ocaramba_selenium bash -c 'pwsh ./scripts/set_AppConfig_for_tests.ps1 "./Ocaramba.Tests.NUnit/bin/Release" "appsettings.Linux.json" "appSettings" "browser|PathToChromeDriverDirectory|protocol|host" "Chrome|/usr/local/bin/|http|the-internet:5000" -logValues -json'
docker exec -u ocaramba ocaramba_selenium bash -c 'mkdir -p /Ocaramba/TestResults && rm -f /Ocaramba/TestResults/Ocaramba.Tests.Docker.trx && dotnet vstest ./Ocaramba.Tests.NUnit/bin/Release/Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory!=NotImplementedInCoreOrUploadDownload)" /Parallel --logger:"trx;LogFileName=Ocaramba.Tests.Docker.trx" --ResultsDirectory:/Ocaramba/TestResults'
$testExitCode = $LASTEXITCODE
docker exec -u ocaramba ocaramba_selenium bash -c 'mkdir -p /tmp/TestOutput && ls ./Ocaramba.Tests.NUnit/bin/Release/TestOutput/ && cp -r ./Ocaramba.Tests.NUnit/bin/Release/TestOutput/. /tmp/TestOutput'
docker cp ocaramba_selenium:/tmp/TestOutput/. ./TestOutput
docker exec -u ocaramba ocaramba_selenium bash -c "cp /Ocaramba/TestResults/Ocaramba.Tests.Docker.trx /tmp/Ocaramba.Tests.Docker.trx"
docker cp ocaramba_selenium:/tmp/Ocaramba.Tests.Docker.trx .
$testReportCopyExitCode = $LASTEXITCODE
docker rm ocaramba_selenium --force

Compress-Archive -Path "./TestOutput/" -DestinationPath "./ExecutingTestsOnDocker$env:GITHUB_RUN_ID.zip"

if ($testReportCopyExitCode -ne 0 -or -not (Test-Path "./Ocaramba.Tests.Docker.trx")) {
    Write-Error "The Docker test run did not produce a TRX results file."
    exit 1
}

$testResults = [xml](Get-Content -Raw "./Ocaramba.Tests.Docker.trx")
$testCounters = $testResults.SelectSingleNode("//*[local-name()='Counters']")
if ($null -eq $testCounters -or [int]($testCounters.GetAttribute("total")) -eq 0) {
    Write-Error "The Docker test run did not discover any tests."
    exit 1
}

exit $testExitCode
