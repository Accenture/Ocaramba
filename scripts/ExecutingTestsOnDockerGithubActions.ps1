docker info
         
docker ps -a
docker exec -u root ocaramba_selenium bash -c "chmod -R 777 /Ocaramba"
docker exec -u ocaramba ocaramba_selenium bash -c "ls ; sed -i '/Documentation/,+5 d' ./Ocaramba.sln ; sed -i '/CloudProviderCrossBrowser/,+5 d' ./Ocaramba.sln; dotnet build ./Ocaramba.sln"
docker exec -u ocaramba ocaramba_selenium bash -c 'pwsh ./scripts/set_AppConfig_for_tests.ps1 "./Ocaramba.Tests.NUnit/bin/Debug/net8.0" "appsettings.Linux.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|/usr/local/bin/" -logValues -json'
docker exec -u ocaramba ocaramba_selenium bash -c 'dotnet vstest ./Ocaramba.Tests.NUnit/bin/Debug/net8.0/Ocaramba.Tests.NUnit.dll /TestCaseFilter:"(TestCategory!=NotImplementedInCoreOrUploadDownload)" /Parallel --logger:trx;LogFileName=Ocaramba.Tests.Docker.trx' 
docker exec -u ocaramba ocaramba_selenium bash -c 'mkdir -p /tmp/TestOutput && ls ./Ocaramba.Tests.NUnit/bin/Debug/net8.0/TestOutput/ && cp -r ./Ocaramba.Tests.NUnit/bin/Debug/net8.0/TestOutput/. /tmp/TestOutput'
docker cp ocaramba_selenium:/tmp/TestOutput/. ./TestOutput
docker exec -u ocaramba ocaramba_selenium bash -c "ls /Ocaramba/TestResults/ && cp /Ocaramba/TestResults/*.trx /tmp/Ocaramba.Tests.Docker.trx"
docker cp ocaramba_selenium:/tmp/Ocaramba.Tests.Docker.trx .
docker rm ocaramba_selenium --force

Compress-Archive -Path "./TestOutput/" -DestinationPath "./ExecutingTestsOnDocker$env:GITHUB_RUN_ID.zip"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
