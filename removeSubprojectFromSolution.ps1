echo "removing Documentation subprojects from .\Ocaramba.sln"

(Get-Content '.\Ocaramba.sln' -raw) -replace [regex]('.*?' + 'Documentation' + ('.*?\r\n' * (5 + 1))) | set-content  '.\Ocaramba.sln'