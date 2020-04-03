echo '********************************************Executing tests********************************************'
docker ps        
docker exec ocaramba_selenium sed -i '/Features/,+1 d' /Ocaramba/Ocaramba.sln
docker exec ocaramba_selenium sed -i '/Documentation/,+5 d' /Ocaramba/Ocaramba.sln
echo 'build Ocaramba.sln'
docker exec ocaramba_selenium dotnet build /Ocaramba/Ocaramba.sln
echo 'execute Ocaramba.sln tests'
docker exec ocaramba_selenium dotnet vstest /Ocaramba/Ocaramba.Tests.NUnit/bin/Debug/netcoreapp3.1/Ocaramba.Tests.NUnit.dll  /TestCaseFilter:"(TestCategory!=NotImplementedInCoreOrUploadDownload)" --logger:"trx;LogFileName=Ocaramba.Tests.Docker.xml"
echo 'Downloading Ocaramba.Tests.Docker.xml'
docker cp  ocaramba_selenium:/Ocaramba/TestResults/Ocaramba.Tests.Docker.xml .
echo 'Uploading Ocaramba.Tests.Docker.xml'

docker rm ocaramba_selenium --force

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0