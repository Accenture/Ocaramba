echo '********************************************Executing tests on Docker********************************************'

if($IsWindows){
docker-switch-linux
         }
docker info
         
#docker-compose up id --build
         
docker ps -a
docker exec ocaramba_selenium bash -c "sed -i '/Features/,+1 d' Ocaramba.sln ; sed -i '/Documentation/,+5 d' Ocaramba.sln ; dotnet build Ocaramba.sln"
docker exec ocaramba_selenium bash -c 'dotnet vstest Ocaramba.Tests.NUnit/bin/Debug/netcoreapp3.1/Ocaramba.Tests.NUnit.dll  /TestCaseFilter:\"(TestCategory!=NotImplementedInCoreOrUploadDownload)\" --logger:\"trx;LogFileName=Ocaramba.Tests.Docker.xml\"' 
docker cp  ocaramba_selenium:/Ocaramba/TestResults/Ocaramba.Tests.Docker.xml .
