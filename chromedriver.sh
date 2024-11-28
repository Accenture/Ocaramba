#!/bin/sh -e

# This implements https://chromedriver.chromium.org/downloads/version-selection
# to install the latest version of ChromeDriver that's compatible with the
# current google-chrome binary.

# Default location where the chromedriver executable will be installed.
# This is a symlink; version-suffixed binaries live alongside it.
destlink=${HOME}/local/bin/chromedriver

# The destination can be overridden via the command line.
if [ $# -ge 1 ]; then
  if [ "$1" = '-h' ] || [ "$1" = '-help' ] || [ "$1" = '--help' ]; then
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

# Check that we'll be able to run the linux64 version of ChromeDriver.
hwname=$(uname -m)
if [ "$hwname" != x86_64 ]; then
  echo "Unsupported hardware name '${hwname}' from 'uname -m'"
  exit 1
fi

# google-chrome --version prints a line like "Google Chrome 96.0.4664.93 " (yes,
# with a trailing space): https://www.chromium.org/developers/version-numbers/
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
  # Starting with Chrome 115, there's an endpoint at that returns a massive JSON
  # object listing all releases.
  re=^${major}\\\\.${minor}\\\\.${build}\\\\.
  url=$(
    wget --quiet -O- https://googlechromelabs.github.io/chrome-for-testing/known-good-versions-with-downloads.json |
    jq -r '.versions[] | select(.version | test("'"${re}"'")) | .downloads.chromedriver[] | select(.platform == "linux64") | .url' |
    tail -1
  )
  if [ -z "$url" ]; then
    echo "Failed finding latest release matching /${re}/"
    exit 1
  fi

  # Extract the release from a URL like this:
  # https://edgedl.me.gvt1.com/edgedl/chrome/chrome-for-testing/119.0.6045.0/linux64/chrome-linux64.zip
  rel=$(echo "$url" | sed -nre 's!.*/([0-9]+\.[0-9]+\.[0-9]+\.[0-9]+)/.*!\1!p')
  if [ -z "$url" ]; then
    echo "Failed getting version number from '${url}'"
    exit 1
  fi

  # The executable also seems to be located in a subdirectory now.
  srcfile=chromedriver-linux64/chromedriver
else
  # We need "MAJOR.MINOR.BUILD", e.g. "96.0.4664". Get the file containing the
  # latest compatible ChromeDriver release number.
  short=$(echo "$build" | sed -re 's/\.[0-9]+$//')
  rel=$(wget --quiet -O- "https://chromedriver.storage.googleapis.com/LATEST_RELEASE_${short}")
  url=https://chromedriver.storage.googleapis.com/${rel}/chromedriver_linux64.zip
  srcfile=chromedriver
fi

echo "Need ChromeDriver ${rel} for Chrome ${cver}"

# Download the appropriate version if we don't already have it.
destfile=${destlink}.${rel}
if [ ! -e "$destfile" ]; then
  tmpdir=$(mktemp --directory -t chromedriver.XXXXXXXXXX)
  zipfile=${tmpdir}/$(basename "$url")
  echo "Downloading ${url} to ${tmpdir}"
  wget --quiet -O "$zipfile" "$url"

  echo "Extracting archive to ${destfile}"
  unzip -q "$zipfile" -d "$tmpdir"
  mv "${tmpdir}/${srcfile}" "./"
  chmod 0755 "chromedriver"
  rm -rf "$tmpdir"
fi

# If there's already a regular file in place of the link, move it away.
if [ -e "$destlink" ] && [ ! -L "$destlink" ]; then
  echo "Moving ${destlink} to ${destlink}.old"
  mv "$destlink" "${destlink}.old"
fi

ls

