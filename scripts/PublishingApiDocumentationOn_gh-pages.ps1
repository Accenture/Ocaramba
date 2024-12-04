echo '********************************************Copying files for gh-pages********************************************'

Copy-Item -Path .\README.md -Destination .\Ocaramba.Documentation\Help\ -recurse -force -verbose 

New-Item .\Ocaramba.Documentation\Help\Ocaramba.Documentation\icons\ -Type Directory

Copy-Item -Path .\Ocaramba.Documentation\icons\Objectivity_logo_avatar.png -Destination .\Ocaramba.Documentation\Help\Ocaramba.Documentation\icons\ -recurse -force -verbose

Copy-Item -Path .\Ocaramba.Documentation\icons\Help.png -Destination .\Ocaramba.Documentation\Help\Ocaramba.Documentation\icons\ -recurse -force -verbose

if ($env:APPVEYOR_REPO_TAG -eq "true")
{
  echo '********************************************Cloning gh-pages branch********************************************'

  Invoke-Expression "git config --global credential.helper store"
  
  Add-Content "$env:USERPROFILE\.git-credentials" "https://$($env:GithubAuthToken):x-oauth-basic@github.com`n"
  
  Invoke-Expression "git config --global core.autocrlf true"
  
  Invoke-Expression "git config --global user.email 'TestAutomationGroup@objectivity.co.uk'"
  
  Invoke-Expression "git config --global user.name 'TestAutomationGroup'"
  
  Invoke-Expression "git clone https://github.com/ObjectivityLtd/Test.Automation.git --branch gh-pages .\Help"
  
  Remove-Item -recurse .\Help\* -exclude .git
  
} else
{

  New-Item -ItemType Directory -Force -Path .\Help
  
}

Copy-Item -Path .\Ocaramba.Documentation\Help\** -Destination .\Help\ -recurse -force
     
if ($env:APPVEYOR_REPO_TAG -eq "true")
{
  echo '********************************************Publishing new version of gh-pages********************************************'
  
  cd .\\Help 
  
  Invoke-Expression "git add --all"
  
  Invoke-Expression "git commit -m 'Publishing to gh-pages $env:appveyor_build_version'"
  
  Invoke-Expression "git push origin gh-pages --porcelain"
  
  cd ..
  
  Remove-Item .\\Help\\.git -Force  -Recurse -ErrorAction SilentlyContinue
  
}

7z a gh-pages_$env:appveyor_build_version.zip .\\Help\\** 

appveyor PushArtifact gh-pages_$env:appveyor_build_version.zip