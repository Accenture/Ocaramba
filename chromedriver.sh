#!/bin/sh -e

# This implements https://chromedriver.chromium.org/downloads/version-selection
# to install the latest version of ChromeDriver that's compatible
# current google-chrome binary.

# Default location where the chromedriver executable will be installed.
# This is a symlink; version-suffixed binaries live alongside it.
destlink=${HOME}/local/bin/chromedriver

# Install jq if not installed
if ! command -v jq &> /dev/null; then
    echo "jq not found, installing..."
    sudo apt-get update
    sudo apt-get install -y jq
fi

# The destination can be overridden via the command line.
if [ $# -ge 1 ]; then
    if [ "$1" = '-h' ]; then
        cat <<EOF >&2
Usage: ${0} [dest-symlink]
Download the appropriate version of ChromeDriver.

The executable will be placed alongside a symlink at
${destlink} by default.
EOF
        exit 2
    fi
    destlink=$1
fi

# Check that we'll be able to run the linux64
hwname=$(uname -m)
if [ "$hwname" != x86_64 ]; then
    echo "Unsupported hardware name '${hwname}' from 'uname -m'"
    exit 1
fi

# google-chrome --version prints a line like "Google Chrome 96.0.4664.93"
vstring=$(google-chrome --version)
cver=$(echo "$vstring" | sed -nre 's/^Google Chrome ([0-9]+\.[0-9]+\.[0-9]+\.[0-9]+)\s*$/\1/p')
if [ -z "$cver" ]; then
    echo "Failed parsing Chrome version '${vstring}'"
    exit 1
fi

major=$(echo "$cver" | cut -d. -f 1)
minor=$(echo "$cver" | cut -d. -f 2)
build=$(echo "$cver" | cut -d. -f 3)

if [ "$major" -ge 115 ]; then
    # Starting with ChromeDriver 115.0.5790.98, we follow the new naming convention.
    url="https://storage.googleapis.com/chrome-for-testing/${cver}/linux64/chromedriver-linux64.zip"
else
    url="https://chromedriver.storage.googleapis.com/${cver}/chromedriver_linux64.zip"
fi

tempdir=$(mktemp -d)
trap "rm -rf ${tempdir}" EXIT

# Download and extract the ChromeDriver
curl -sSL "$url" -o "${tempdir}/chromedriver.zip"
unzip -q "${tempdir}/chromedriver.zip" -d "${tempdir}"

# Ensure the destination directory exists
mkdir -p "$(dirname "${destlink}")"

# Move the ChromeDriver to the destination directory
mv "${tempdir}/chromedriver-linux64/chromedriver" "${destlink}.${cver}"
ln -sf "${destlink}.${cver}" "${destlink}"
