#!/bin/bash

check_port() {
    if nc -z localhost $1; then
        echo "servers already open" 
    else
        echo "starting server on $1."
        # Put the command you want to execute here
        (dotnet run --project csharp/back/back.csproj | tee /dev/tty) &
        (dotnet run --project csharp/middle/middle.csproj | tee /dev/tty) &
        (dotnet run --project csharp/front/front.csproj | tee /dev/tty) &
    fi
}

check_port 5272


wait

echo "All servers closed."
