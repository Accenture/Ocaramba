#echo 'Update Chrome'
#wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | sudo apt-key add -
#sudo sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'
#sudo apt-get update
#sudo apt-get install google-chrome-stable
$ChromeDriverVersion=google-chrome --product-version;
echo ChromeDriverVersion: $ChromeDriverVersion
$TempFilePath = [System.IO.Path]::GetTempFileName();
$TempZipFilePath = $TempFilePath.Replace(".tmp", ".zip");
Rename-Item -Path $TempFilePath -NewName $TempZipFilePath;
$TempFileUnzipPath = $TempFilePath.Replace(".tmp", "");
Invoke-WebRequest "https://chromedriver.storage.googleapis.com/$ChromeDriverVersion/chromedriver_linux64.zip" -OutFile $TempZipFilePath;
Expand-Archive $TempZipFilePath -DestinationPath $TempFileUnzipPath;
echo TempFileUnzipPath: $TempFileUnzipPath
dir $TempFileUnzipPath

.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnit\bin\Debug\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Angular\bin\Debug\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.NUnitExtentReports\bin\Debug\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Features\bin\Debug\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.Xunit\bin\Debug\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json 
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.Tests.MsTest\bin\Debug\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json
.\scripts\set_AppConfig_for_tests.ps1 ".\Ocaramba.UnitTests\bin\Debug\net8.0" "appsettings.json" "appSettings" "browser|PathToChromeDriverDirectory" "Chrome|$TempFileUnzipPath" -logValues -json

echo '********************************************Executing tests********************************************'

echo '********************************************NUnit tests********************************************'   
dotnet test --configuration Debug --filter TestCategory!=NotImplementedInCoreOrUploadDownload --no-build --no-restore Ocaramba.UnitTests -maxCpuCount --test-adapter-path:. --logger:Appveyor

dotnet test --configuration Debug --no-build --no-restore Ocaramba.Tests.Angular -maxCpuCount --test-adapter-path:. --logger:Appveyor

dotnet test --configuration Debug --filter TestCategory!=NotImplementedInCoreOrUploadDownload --no-build --no-restore Ocaramba.Tests.NUnit -maxCpuCount --test-adapter-path:. --logger:Appveyor  

echo '********************************************XUnit tests********************************************' 

dotnet test --configuration Debug --no-build --no-restore Ocaramba.Tests.Xunit -maxCpuCount --test-adapter-path:. --logger:Appveyor

echo '********************************************Specflow tests********************************************' 

#dotnet test --configuration Debug --filter TestCategory!=NotImplementedInCoreOrUploadDownload --no-build --no-restore Ocaramba.Tests.Features -maxCpuCount --test-adapter-path:. --logger:Appveyor  


echo '********************************************MsTest tests********************************************'   

dotnet test --configuration Debug --no-build --no-restore Ocaramba.Tests.MsTest -maxCpuCount --test-adapter-path:. --logger:Appveyor
    
if($lastexitcode -ne 0)
  {
   echo 'lastexitcode' $lastexitcode
  }
  
7z a testresults_Ubuntu.zip ./Ocaramba.*/bin/Debug/**/TestOutput/*.png

7z a testresults_Ubuntu.zip ./Ocaramba.*/bin/Debug/**/TestOutput/*.html

7z a testresults_Ubuntu.zip ./Ocaramba.*/bin/Debug/**/*.log

appveyor PushArtifact testresults_Ubuntu.zip
