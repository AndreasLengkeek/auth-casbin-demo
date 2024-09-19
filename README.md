# Casbin Auth project


## Setup

### Prerequisite

- Dotnet 8
- EF Core
- Psql database

### Steps

1. Apply migrations for both database contexts

 `dotnet ef database update --context ApplicationDbContext`

 `dotnet ef database update --context CasbinDatabaseContext`
1. Run with `dotnet run`