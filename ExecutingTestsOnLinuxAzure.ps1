echo 'Update Chrome'
wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | sudo apt-key add -
sudo sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'
sudo apt-get update
sudo apt-get install google-chrome-stable

echo '********************************************Executing tests********************************************'
        
echo '********************************************netcoreapp3 tests********************************************'

$Env:ASPNETCORE_ENVIRONMENT="Linux"

echo $Env:ASPNETCORE_ENVIRONMENT

dotnet vstest ./Ocaramba.Tests.NUnit/bin/Release/netcoreapp3.1/Ocaramba.Tests.NUnit.dll `
			  /TestCaseFilter:"(TestCategory!=TakingScreehShots)" /Parallel `
	          --logger:"trx;LogFileName=Ocaramba.Tests.netcoreapp.xml"

if($lastexitcode -ne 0)
 {
  echo 'lastexitcode' $lastexitcode
 }
 
exit 0
