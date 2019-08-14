echo '********************************************Executing tests********************************************'

echo '********************************************NUnit tests********************************************'   
dotnet test --configuration Release --no-build --no-restore Ocaramba.Tests.Angular -maxCpuCount --test-adapter-path:. --logger:Appveyor

dotnet test --configuration Release --filter TestCategory!=NotImplementedInCoreOrUploadDownload --no-build --no-restore Ocaramba.Tests.NUnit -maxCpuCount --test-adapter-path:. --logger:Appveyor  

dotnet test --configuration Release --filter TestCategory!=NotImplementedInCoreOrUploadDownload --no-build --no-restore Ocaramba.UnitTests -maxCpuCount --test-adapter-path:. --logger:Appveyor

echo '********************************************XUnit tests********************************************' 

dotnet test --configuration Release --no-build --no-restore Ocaramba.Tests.Xunit -maxCpuCount --test-adapter-path:. --logger:Appveyor

echo '********************************************Specflow tests********************************************' 

dotnet test --configuration Release --no-build --no-restore Ocaramba.Tests.Features -maxCpuCount --test-adapter-path:. --logger:Appveyor

echo '********************************************MsTest tests********************************************'   

dotnet test --configuration Release --no-build --no-restore Ocaramba.Tests.MsTest -maxCpuCount --test-adapter-path:. --logger:Appveyor
    
if($lastexitcode -ne 0)
  {
   echo 'lastexitcode' $lastexitcode
  }
  
7z a testresults_Ubuntu.zip ./Ocaramba.*/bin/Release/**/TestOutput/*.png

7z a testresults_Ubuntu.zip ./Ocaramba.*/bin/Release/**/TestOutput/*.html

7z a testresults_Ubuntu.zip ./Ocaramba.*/bin/Release/**/*.log

appveyor PushArtifact testresults_Ubuntu.zip