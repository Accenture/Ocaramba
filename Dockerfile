FROM mcr.microsoft.com/dotnet/sdk:6.0
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
# Chrome driver
#=========
RUN CHROMEVER=$(google-chrome --product-version | grep -o "[^\\.]*\\.[^\\.]*\\.[^\\.]*") \
    && DRIVERVER=$(curl -s "https://chromedriver.storage.googleapis.com/LATEST_RELEASE_$CHROMEVER") \
    && if [ -z "$DRIVERVER" ]; then echo "Failed to get ChromeDriver version"; exit 1; fi \
    && wget -q --continue -P /chromedriver "http://chromedriver.storage.googleapis.com/$DRIVERVER/chromedriver_linux64.zip" \
    && unzip /chromedriver/chromedriver* -d /chromedriver
#=========
# Firefox
#=========
RUN wget --no-verbose -O /tmp/firefox.tar.bz2 'https://download.mozilla.org/?product=firefox-latest-ssl&os=linux64&lang=en-US' \
&& lbzip2 -d /tmp/firefox.tar.bz2 &&  tar -C /opt -xvf /tmp/firefox.tar && rm /tmp/firefox.tar  && ln -fs /opt/firefox/firefox /usr/bin/firefox \
&& apt-get install libdbus-glib-1-2 
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
