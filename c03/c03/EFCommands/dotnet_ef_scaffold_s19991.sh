#!/bin/bash


## dodaje tabelki za pomoca EF do EntityModels/
dotnet ef dbcontext scaffold "Data Source=db-mssql.pjwstk.edu.pl; Initial Catalog=s19991; User Id=apbds19991; Password=admin" Microsoft.EntityFrameworkCore.SqlServer -o EntityModels/
