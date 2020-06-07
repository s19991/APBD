#!/bin/bash
export PATH="$PATH:/home/fabians/.dotnet/tools"

# Dodaje wybrane tabelki do Models za pomoca scaffold
CONNECTION_STRING="\"Data Source=db-mssql.pjwstk.edu.pl; Initial Catalog=s19991; User Id=apbds19991; Password=admin\""
dotnet ef dbcontext scaffold $CONNECTION_STRING Microsoft.EntityFrameworkCore.SqlServer -o Models/ \
-t Campaign \
-t Banner \
-t Building \
-t Client 
