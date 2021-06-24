echo "removing Documentation subprojects from .\Ocaramba.sln"
echo $isWindows
#if (-Not $isWindows) {    
echo "removing Documentation subprojects from .\Ocaramba.sln"
(Get-Content '.\Ocaramba.sln' -raw) -replace [regex]('.*?' + 'Documentation' + ('.*?\n' * (5 + 1))) | set-content  '.\Ocaramba.sln'
#	}
