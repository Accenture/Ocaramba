FROM mcr.microsoft.com/dotnet/sdk:8.0
LABEL maintainer "Ocaramba <ocaramba@objectivity.co.uk>"
#=========
# Midnight Commander, lbzip2
#=========
RUN apt-get update && apt-get install -y && apt-get --yes install mc && apt-get --yes install lbzip2 && apt-get --yes install jq
#=========
# Chrome
#=========
RUN apt-get update && apt-get install -y xdg-utils libxrandr2
RUN apt-get update && apt-get install -y gnupg \
    && wget -q -O - https://dl.google.com/linux/linux_signing_key.pub | apt-key add - \
    && sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" > /etc/apt/sources.list.d/google-chrome.list' \
    && apt-get update && apt-get install -y google-chrome-stable
#=========
# Microsoft Edge
#=========
RUN apt-get update && apt-get install -y wget gnupg \
    && wget -q -O - https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg \
    && install -o root -g root -m 644 microsoft.gpg /etc/apt/trusted.gpg.d/ \
    && sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/edge stable main" > /etc/apt/sources.list.d/microsoft-edge.list' \
    && rm microsoft.gpg \
    && apt-get update \
    && apt-get install -y microsoft-edge-stable
#=========
# Edge driver
#=========
RUN EDGEBROWSER_VERSION=$(microsoft-edge --version | awk '{print $NF}') \
    && EDGE_MAJOR=$(echo $EDGEBROWSER_VERSION | cut -d. -f1) \
    && EDGE_DRIVER_URL="https://msedgedriver.azureedge.net/$EDGEBROWSER_VERSION/edgedriver_linux64.zip" \
    && wget -O /tmp/edgedriver.zip "$EDGE_DRIVER_URL" \
    && unzip /tmp/edgedriver.zip -d /usr/local/bin/ \
    && mv /usr/local/bin/msedgedriver /usr/local/bin/edgedriver \
    && chmod +x /usr/local/bin/edgedriver \
    && rm /tmp/edgedriver.zip
#=========
# Chrome driver
#=========
RUN CHROMEVER=$(google-chrome --product-version | grep -oE "[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+") \
    && major=$(echo "$CHROMEVER" | cut -d. -f 1) \
    && minor=$(echo "$CHROMEVER" | cut -d. -f 2) \
    && build=$(echo "$CHROMEVER" | cut -d. -f 3) \
    && if [ "$major" -ge 115 ]; then \
        re=^${major}\.${minor}\.${build}\.; \
        url=$(wget --quiet -O- https://googlechromelabs.github.io/chrome-for-testing/known-good-versions-with-downloads.json | \
              jq -r '.versions[] | select(.version | test("'"${re}"'")) | .downloads.chromedriver[] | select(.platform == "linux64") | .url' | \
              tail -1); \
        if [ -z "$url" ]; then \
            echo "Failed finding latest release matching /${re}/"; \
            exit 1; \
        fi; \
        rel=$(echo "$url" | sed -nre 's!.*/([0-9]+\.[0-9]+\.[0-9]+\.[0-9]+)/.*!\1!p'); \
        srcfile=chromedriver-linux64/chromedriver; \
    else \
        short=$(echo "$build" | sed -re 's/\.[0-9]+$//'); \
        rel=$(wget --quiet -O- "https://chromedriver.storage.googleapis.com/LATEST_RELEASE_${short}"); \
        url=https://chromedriver.storage.googleapis.com/${rel}/chromedriver_linux64.zip; \
        srcfile=chromedriver; \
    fi \
    && wget -q --continue -P /chromedriver "$url" \
    && unzip /chromedriver/chromedriver* -d /chromedriver \
    && mv /chromedriver/$srcfile /usr/local/bin/chromedriver \
    && chmod +x /usr/local/bin/chromedriver
#=========
# Firefox
#=========
RUN wget --no-verbose -O /tmp/firefox.tar.xz 'https://download.mozilla.org/?product=firefox-latest-ssl&os=linux64&lang=en-US' \
    && tar -C /opt -xJf /tmp/firefox.tar.xz \
    && rm /tmp/firefox.tar.xz \
    && ln -fs /opt/firefox/firefox /usr/bin/firefox
#=========
# Firefox driver
#=========
RUN BASE_URL=https://github.com/mozilla/geckodriver/releases/download \
  && VERSION=$(curl -sL \
    https://api.github.com/repos/mozilla/geckodriver/releases/latest | \
     jq -r '.tag_name') \
  && curl -sL "$BASE_URL/$VERSION/geckodriver-$VERSION-linux64.tar.gz" | \
    tar -xz -C /usr/local/bin

ENV ASPNETCORE_ENVIRONMENT Linux
#=========
# Create a user and switch to it
#=========
RUN groupadd -r ocaramba && useradd -r -g ocaramba ocaramba && mkdir -p /home/ocaramba && chown -R ocaramba:ocaramba /home/ocaramba
USER ocaramba
WORKDIR /home/ocaramba
#=========
# Health Check
#=========
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
  CMD dotnet --info || exit 1
