param (
    [Parameter(Mandatory=$false)]
    [ValidateSet('start', 'stop')]
    [string]$Action = 'start',
    
    [Parameter(Mandatory=$false)]
    [string]$EmulatorName = "test_device",
    
    [Parameter(Mandatory=$false)]
    [int]$AppiumPort = 4723,

    [Parameter(Mandatory=$false)]
    [string]$AndroidSdkRoot = "$env:LOCALAPPDATA\Android\Sdk"
)

# Set up Android SDK paths
$env:ANDROID_HOME = $AndroidSdkRoot
$env:PATH = "$AndroidSdkRoot\platform-tools;$AndroidSdkRoot\emulator;$AndroidSdkRoot\tools;$AndroidSdkRoot\tools\bin;$env:PATH"

# Verify Android SDK tools are available
$adbPath = Join-Path $AndroidSdkRoot "platform-tools\adb.exe"
$emulatorPath = Join-Path $AndroidSdkRoot "emulator\emulator.exe"

if (-not (Test-Path $adbPath)) {
    Write-Error "ADB not found at: $adbPath. Please install Android SDK platform-tools."
    exit 1
}

if (-not (Test-Path $emulatorPath)) {
    Write-Error "Android emulator not found at: $emulatorPath. Please install Android SDK emulator."
    exit 1
}

function Start-AppiumServer {
    $appiumJob = Start-Job -ScriptBlock {
        param($port)
        appium --base-path /wd/hub --port $port
    } -ArgumentList $AppiumPort

    Write-Host "Appium server started on port $AppiumPort"
    Write-Host "Job ID: $($appiumJob.Id)"
    
    # Wait for Appium to start
    Start-Sleep -Seconds 5
}

function Start-AndroidEmulator {
    param($emulatorName)
    
    # Check if emulator is already running
    $adbDevices = & "$adbPath" devices
    if ($adbDevices -match "emulator") {
        Write-Host "Emulator is already running"
        return
    }

    Write-Host "Starting Android emulator: $emulatorName"
    Start-Process $emulatorPath -ArgumentList "-avd $emulatorName -no-audio -gpu swiftshader_indirect -no-snapshot -noaudio -no-boot-anim" -NoNewWindow

    Write-Host "Waiting for emulator to boot..."
    & "$adbPath" wait-for-device
    & "$adbPath" shell "while [[ -z $(getprop sys.boot_completed) ]]; do sleep 1; done;"
    Write-Host "Emulator is ready"
}

function Stop-AppiumServer {
    Get-Job | Where-Object { $_.Command -like "*appium*" } | Stop-Job
    Get-Job | Where-Object { $_.Command -like "*appium*" } | Remove-Job
    Write-Host "Appium server stopped"
}

function Stop-AndroidEmulator {
    & "$adbPath" devices | ForEach-Object {
        if ($_ -match "emulator") {
            $device = ($_ -split "\s+")[0]
            & "$adbPath" -s $device emu kill
        }
    }
    Write-Host "Android emulator stopped"
}

switch ($Action) {
    'start' {
        Start-AppiumServer
        Start-AndroidEmulator -emulatorName $EmulatorName
    }
    'stop' {
        Stop-AppiumServer
        Stop-AndroidEmulator
    }
}
