#!/usr/bin/bash
export PATH="$PATH:/home/fabians/.dotnet/tools"

dotnet ef migrations add Kolokwium2 -o Migrations
dotnet ef database update

