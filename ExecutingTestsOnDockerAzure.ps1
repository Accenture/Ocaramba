docker info
         
docker ps -a
docker exec ocaramba_selenium bash -c "ls ; sed -i '/Documentation/,+5 d' ./Ocaramba.sln ; dotnet build ./Ocaramba.sln"
docker exec ocaramba_selenium bash -c 'pwsh ./scripts/set_AppConfig_for_tests.ps1 \"./Ocaramba.Tests.NUnit/bin/Debug/netcoreapp3.1\" \"appsettings.Linux.json\" \"appSettings\" \"browser|PathToChromeDriverDirectory\" \"Chrome|/chromedriver\" $true $true'
docker exec ocaramba_selenium bash -c 'dotnet vstest ./Ocaramba.Tests.NUnit/bin/Debug/netcoreapp3.1/Ocaramba.Tests.NUnit.dll /TestCaseFilter:\"(TestCategory!=NotImplementedInCoreOrUploadDownload)\" /Parallel --logger:\"trx;LogFileName=Ocaramba.Tests.Docker.xml\"' 

docker cp  ocaramba_selenium:/Ocaramba/TestResults/Ocaramba.Tests.Docker.xml .


docker rm ocaramba_selenium --force

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0