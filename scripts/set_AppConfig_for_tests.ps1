<#
The MIT License (MIT)
Copyright (c) 2015 Objectivity Bespoke Software Specialists
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
#>
    param(
    [string]$OutDir,
    
    [string]$configName,

    [string]$section,
    
    [string]$keys,
    
    [string]$values,
	
	[switch]$logValues,
	
	[switch]$json
    )

Function set_AppConfig_for_tests([string]$OutDir,[string]$configName,[string]$section,[string]$keys,[string]$values, [bool]$logValues, [bool]$json)
{
	    <#
    .SYNOPSIS
    Update App.config file.
    .DESCRIPTION
    Update App.config file for given keys and keys values in given section.
    
    .PARAMETER OutDir
    Working directory.
    .PARAMETER configName
    Name of config file for update.
    .PARAMETER section
    Name of section in config file for update.
    .PARAMETER keys
    Names of keys for update separated by |.
    .PARAMETER values
    Values of keys for update separated by |, in same order as keys.
    .EXAMPLE
    .\set_AppConfig_for_tests "..\Ocaramba.Tests.BrowserStackCrossBrowser\bin\Debug" "Ocaramba.Tests.NUnit.dll.config" "//DriverCapabilities" "browserstack.user|browserstack.key" "key1value|key2value"
    .\set_AppConfig_for_tests.ps1 "..\Ocaramba.Tests.BrowserStackCrossBrowser\bin\Debug" "Ocaramba.Tests.NUnit.dll.config" "//DriverCapabilities" "browserstack.user|browserstack.key" "key1value|key2value" $true
    #>


	$workingDir = Resolve-Path $OutDir
    Write-Host OutDir $OutDir
    Write-Host workingDir $workingDir
    Write-Host configName $configName
    Write-Host section $section
    Write-Host keys $keys
	Write-Host logValues $logValues
	if($logValues -eq $true)
	{
		Write-Host values $values
	}
    Write-Host json $json
    $configFile = "$workingDir\$configName"
    Write-Host configFile $configFile
	[string[]]$keysArray=$keys.Split('|')
	[string[]]$valueArray=$values.Split('|')
    if($json -eq $false)
	{
			[xml]$config = Get-Content -Path $configFile


			for($i=0; $i -lt $keysArray.length; $i++)
			  {

				$key=$keysArray[$i]
				Write-Host  "key:" $i $key
				$value=$valueArray[$i]
				if($logValues -eq $true)
				{
					Write-Host "value:" $i $value
				}
				$config.SelectSingleNode("/configuration$section//add[@key='$key']/@value").value = $value

			  }	
			$config.Save($configFile)
	} else
	{
			$appsettings = Get-Content $configFile -raw | ConvertFrom-Json 
            for($i=0; $i -lt $keysArray.length; $i++)
			  {

				$key=$keysArray[$i]
				Write-Host  "key:" $i $key
				$value=$valueArray[$i]
				if($logValues -eq $true)
				{
					Write-Host "value:" $i $value
				}
				$appsettings.$section.$key= $value
			  }
			$appsettings | ConvertTo-Json -depth 32| set-content $configFile
	}
}
set_AppConfig_for_tests $OutDir $configName $section $keys $values $logValues $json