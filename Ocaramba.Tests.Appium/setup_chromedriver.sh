#!/usr/bin/env bash
set -euo pipefail
CHROME_VERSION=$(adb shell dumpsys package com.google.android.webview | grep -m1 versionName | cut -d= -f2)
echo "WebView Chrome version $CHROME_VERSION" 
CHROME_MAJOR=$(echo "$CHROME_VERSION" | cut -d. -f1-3) 
echo "Detected WebView Chrome version: major $CHROME_MAJOR" 
DATA=$(curl -s https://googlechromelabs.github.io/chrome-for-testing/latest-patch-versions-per-build.json)

# Get the latest patch version for that build
PATCH_VERSION=$(echo "$DATA" | jq -r ".builds[\"$CHROME_MAJOR\"].version")

echo "Latest patch version for $CHROME_MAJOR is $PATCH_VERSION"
echo "Latest Chromedriver for Chrome $CHROME_MAJOR is $PATCH_VERSION" 
wget -O chromedriver.zip "https://storage.googleapis.com/chrome-for-testing-public/$PATCH_VERSION/linux64/chromedriver-linux64.zip" 
unzip -o chromedriver.zip -d "./Ocaramba.Tests.Appium/bin/Release/net8.0/" 
chmod +x "./Ocaramba.Tests.Appium/bin/Release/net8.0/chromedriver" 
echo "./Ocaramba.Tests.Appium/bin/Release/net8.0/chromedriver is set up"