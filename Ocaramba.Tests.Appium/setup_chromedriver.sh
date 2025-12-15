#!/usr/bin/env bash
set -euo pipefail
CHROME_VERSION=$(adb shell dumpsys package com.google.android.webview | grep -m1 versionName | cut -d= -f2)
echo "WebView Chrome version $CHROME_VERSION" 
CHROME_MAJOR=$(echo "$CHROME_VERSION" | cut -d. -f1-3) 
echo "Detected WebView Chrome version: major $CHROME_MAJOR" 
LATEST_DRIVER=$(curl -s "https://chromedriver.storage.googleapis.com/LATEST_RELEASE_$CHROME_MAJOR") 
echo "Latest Chromedriver for Chrome $CHROME_MAJOR is $LATEST_DRIVER" 
wget -O chromedriver.zip "https://chromedriver.storage.googleapis.com/$LATEST_DRIVER/chromedriver_linux64.zip" 
unzip -o chromedriver.zip -d "$HOME/chromedriver" 
chmod +x "$HOME/chromedriver/chromedriver" 
echo "$HOME/chromedriver"