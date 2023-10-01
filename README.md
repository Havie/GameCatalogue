# GameCatalogue
 Local .NET and Blazor SPA application to simulate a video game catalogue. 
 
 + ASP.NET
 + Data Binding 
 + Server and Client Data Validation
 + Dependency Injection
 + Bootstrap for CSS
 + CRUD 
 + Docker
 + SQL


 ## Starting SQL server Docker container
 ```powershell
 $sa_password = "SA PASSWORD HERE"
 #-d -v sqlvolume:/var/opt/mssql -- rm --name gameSql = Store the sql data on the outside volume (persist beyond docker container)
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=test!!" -p 1433:1433 -d -v sqlvolume:/var/opt/mssql --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
 ```