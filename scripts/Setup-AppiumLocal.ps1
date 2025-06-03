param (
    [string]$AndroidSdkPath = "$env:LOCALAPPDATA\Android\Sdk",
    [string]$JavaHome = "$env:JAVA_HOME",
    [string]$EmulatorName = "test_device"
)

Write-Host "Setting up local Appium environment..."

# Check Node.js installation
if (-not (Get-Command node -ErrorAction SilentlyContinue)) {
    Write-Error "Node.js is not installed. Please install Node.js from https://nodejs.org/"
    exit 1
}

# Check Java installation
if (-not $JavaHome) {
    Write-Error "JAVA_HOME is not set. Please install Java JDK and set JAVA_HOME"
    exit 1
}

# Install Appium and dependencies
Write-Host "Installing Appium and dependencies..."
npm install -g appium
npm install -g appium-doctor

# Run Appium Doctor to verify setup
Write-Host "Running Appium Doctor to verify setup..."
appium-doctor --android

# Install Appium drivers
Write-Host "Installing Appium drivers..."
appium driver install uiautomator2
appium driver install chromium

# Set up Android environment
if ($AndroidSdkPath) {
    Write-Host "Setting up Android environment..."
    $env:ANDROID_HOME = $AndroidSdkPath
    [System.Environment]::SetEnvironmentVariable("ANDROID_HOME", $AndroidSdkPath, [System.EnvironmentVariableTarget]::User)
    
    # Add Android SDK paths to system PATH
    $paths = @(
        "$AndroidSdkPath\platform-tools",
        "$AndroidSdkPath\emulator",
        "$AndroidSdkPath\cmdline-tools\latest\bin"
    )
    
    foreach ($path in $paths) {
        if (Test-Path $path) {
            $currentPath = [Environment]::GetEnvironmentVariable("Path", "User")
            if ($currentPath -notlike "*$path*") {
                [Environment]::SetEnvironmentVariable("Path", "$currentPath;$path", "User")
            }
        }
    }
    
    # Refresh environment variables in current session
    $env:Path = [System.Environment]::GetEnvironmentVariable("Path", "User")

    # Install required Android SDK packages
    if (Test-Path "$AndroidSdkPath\cmdline-tools\latest\bin\sdkmanager.bat") {
        Write-Host "Installing required Android SDK packages..."
        $sdkmanager = "$AndroidSdkPath\cmdline-tools\latest\bin\sdkmanager.bat"
        
        Write-Host "Accepting Android SDK licenses..."
        echo "y" | & $sdkmanager --licenses
        
        # Install required packages
        $packages = @(
            "platform-tools",
            "platforms;android-30",
            "system-images;android-30;google_apis;x86_64",
            "build-tools;30.0.3"
        )
        
        foreach ($package in $packages) {
            Write-Host "Installing $package..."
            echo "y" | & $sdkmanager $package
        }
        
        # Create AVD if it doesn't exist
        $avdmanager = "$AndroidSdkPath\cmdline-tools\latest\bin\avdmanager.bat"
        if (Test-Path $avdmanager) {
            if (-not (& $avdmanager list avd | Select-String $EmulatorName)) {
                Write-Host "Creating Android Virtual Device '$EmulatorName'..."
                echo "no" | & $avdmanager create avd --force `
                    -n $EmulatorName `
                    -k "system-images;android-30;google_apis;x86_64" `
                    -d "pixel_2"
            } else {
                Write-Host "Android Virtual Device '$EmulatorName' already exists"
            }
        }
    } else {
        Write-Warning "Android SDK Manager not found at expected location. Please install Android Studio and ensure cmdline-tools are installed."
    }
}

Write-Host "Setup complete! You can now run Appium tests locally."
Write-Host "To start Appium server: appium --base-path /wd/hub"
Write-Host "To start Android emulator: emulator -avd $EmulatorName"
