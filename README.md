# MinimalAPI
Minimal API template + Entity Framework

## Stack

1. NET 7
1. Entity Framework
1. Sql Server 


## EF Migrations

1. Install Ef tools
```
    Install-Package Microsoft.EntityFrameworkCore.Tools
```

2. Create your first Migration
```
    dotnet ef migrations add InitialCreate
```
3. Update Database
```
    dotnet ef database update
```
