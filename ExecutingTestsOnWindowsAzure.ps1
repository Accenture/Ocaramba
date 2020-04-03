echo '********************************************Executing tests********************************************'

docker info

docker ps -a
docker exec ocaramba_selenium sed -i '/Features/,+1 d' Ocaramba.sln
docker exec ocaramba_selenium sed -i '/Documentation/,+5 d' Ocaramba.sln
docker exec ocaramba_selenium dotnet build Ocaramba.sln
docker exec ocaramba_selenium dotnet vstest Ocaramba.Tests.NUnit/bin/Debug/netcoreapp3.1/Ocaramba.Tests.NUnit.dll  /TestCaseFilter:"(TestCategory!=NotImplementedInCoreOrUploadDownload)" --logger:"trx;LogFileName=Ocaramba.Tests.Docker.xml"

docker cp  ocaramba_selenium:/Ocaramba/TestResults/Ocaramba.Tests.Docker.xml .



if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0