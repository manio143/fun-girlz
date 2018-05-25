FROM microsoft/dotnet:2.0-sdk

RUN apt-get update && apt-get upgrade -y 

RUN apt-get install fsharp -y

RUN apt-get install curl -y

WORKDIR /tmp/

# install VS Code
RUN apt-get install libnotify4 libnss3 libxkbfile1 libgconf-2-4 libsecret-1-0 -y
RUN curl -L "https://go.microsoft.com/fwlink/?LinkID=760868" -o vscode.deb
RUN dpkg -i vscode.deb

# get missing libraries
RUN apt-get install libgtk2.0-0 libxss1 libasound2 -y

RUN mkdir -p /home/dev
WORKDIR /home/dev

VOLUME /home/dev/workdir

CMD code -w /home/dev/workdir --user-data-dir /home/dev

LABEL maintainer=marian.dziubiak@gmail.com
