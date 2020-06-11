#!/usr/bin/env bash
export PATH="$PATH:/home/fabians/.dotnet/tools"

dotnet ef migrations add AddTables
dotnet ef database update
