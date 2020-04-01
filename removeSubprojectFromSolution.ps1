
if ($env:isLinux) {
echo "removing Features subprojects from .\Ocaramba.sln"
(Get-Content '.\Ocaramba.sln' -raw) -replace [regex]('.*?' + 'Features' + ('.*?\n' * (5 + 1))) | set-content  '.\Ocaramba.sln'
	
} 
echo "removing Documentation subprojects from .\Ocaramba.sln"
(Get-Content '.\Ocaramba.sln' -raw) -replace [regex]('.*?' + 'Documentation' + ('.*?\n' * (5 + 1))) | set-content  '.\Ocaramba.sln'
	
