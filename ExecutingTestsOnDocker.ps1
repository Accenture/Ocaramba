echo '********************************************Executing tests on Docker********************************************'
If($IsWindows){
	docker-switch-linux
}

docker info
         
docker build -t ocaramba/selenium -f DockerfileBuild .
         
docker run --rm -dit --name ocaramba_selenium ocaramba/selenium
docker ps -a

docker exec ocaramba_selenium sed -i '/Documentation/,+5 d' Ocaramba.sln
echo 'build Ocaramba.sln'
docker exec ocaramba_selenium dotnet build Ocaramba.sln
echo 'execute Ocaramba.sln tests'
docker exec ocaramba_selenium dotnet vstest Ocaramba.Tests.NUnit/bin/Debug/netcoreapp3.1/Ocaramba.Tests.NUnit.dll  /TestCaseFilter:"(TestCategory!=NotImplementedInCoreOrUploadDownload)" /Parallel --logger:"trx;LogFileName=Ocaramba.Tests.Docker.xml"
echo 'Downloading Ocaramba.Tests.Docker.xml'
docker cp  ocaramba_selenium:/Ocaramba/TestResults/Ocaramba.Tests.Docker.xml .
echo 'Uploading Ocaramba.Tests.Docker.xml'
$wc = New-Object 'System.Net.WebClient'
$wc.UploadFile("https://ci.appveyor.com/api/testresults/xunit/$($env:APPVEYOR_JOB_ID)", (Resolve-Path .\Ocaramba.Tests.Docker.xml))

docker rm ocaramba_selenium --force


