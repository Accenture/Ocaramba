FROM microsoft/dotnet:2.2-sdk-stretch
LABEL maintainer "Ocaramba <ocaramba@objectivity.co.uk>"
#=========
# Midnight Commander
#=========
RUN apt-get update && apt-get install -y && apt-get --yes install mc
#=========
# Chrome
#=========
RUN wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
RUN dpkg -i google-chrome-stable_current_amd64.deb; apt-get -fy install
ENV ASPNETCORE_ENVIRONMENT Linux
