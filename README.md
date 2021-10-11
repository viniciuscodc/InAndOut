# InAndOut
This is a mvc core web application made using .net 5. 

.Net SDK version: 5.0.301

# Preview

<img src="preview.gif" alt="drawing"/>

# Docker
The url to launch application is https://localhost:5001
```
docker-compose build
```
```
docker-compose up
```

# Running without docker
The entity framework core was used to manage the database. To adjust the database the connection string in InAndOut.csproj must be modified for the database used. Entity framework can be installed to generate tables automatically:

Visual studio:

```
Add-Migration "name"
```

```
Update-database
```

Cli:

```
dotnet tool install --global dotnet ef
```

```
dotnet ef migrations add "name"
```

```
dotnet ef database update
```

To run the application a .net runtime must be installed, it's available for all platforms. It can be run using visual studio or via CLI in the root folder:

```
dotnet run
```
