docker info
         
docker ps -a
docker exec ocaramba_selenium bash -c "ls ; sed -i '/Features/,+1 d' ./Ocaramba.sln ; sed -i '/Documentation/,+5 d' ./Ocaramba.sln ; dotnet build ./Ocaramba.sln"
docker exec ocaramba_selenium bash -c 'dotnet vstest ./Ocaramba.Tests.NUnit/bin/Debug/netcoreapp3.1/Ocaramba.Tests.NUnit.dll `
./Ocaramba.Tests.Angular/bin/Debug/netcoreapp3.1/Ocaramba.Tests.Angular.dll ./Ocaramba.Tests.MsTest/bin/Debug/netcoreapp3.1/Ocaramba.Tests.MsTest.dll `
./Ocaramba.UnitTests/bin/Debug/netcoreapp3.1/Ocaramba.UnitTests.dll ./Ocaramba.Tests.Xunit/bin/Debug/netcoreapp3.1/Ocaramba.Tests.Xunit.dll `
/TestCaseFilter:\"(TestCategory!=NotImplementedInCoreOrUploadDownload)\" `
--logger:\"trx;LogFileName=Ocaramba.Tests.Docker.xml\"' 

docker cp  ocaramba_selenium:/Ocaramba/TestResults/Ocaramba.Tests.Docker.xml .


docker rm ocaramba_selenium --force

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0