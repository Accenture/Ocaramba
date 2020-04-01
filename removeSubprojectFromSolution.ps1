echo "removing Documentation subprojects from .\Ocaramba.sln"

if ($env:isWindows) {

	(Get-Content '.\Ocaramba.sln' -raw) -replace [regex]('.*?' + 'Documentation' + ('.*?\n' * (5 + 1))) | set-content  '.\Ocaramba.sln'
	
} else {

	(Get-Content '.\Ocaramba.sln' -raw) -replace [regex]('.*?' + 'Documentation' + ('.*?\r\n' * (5 + 1))) | set-content  '.\Ocaramba.sln'
	(Get-Content '.\Ocaramba.sln' -raw) -replace [regex]('.*?' + 'Features' + ('.*?\r\n' * (5 + 1))) | set-content  '.\Ocaramba.sln'
}