# TodoApi

## Quick start
```
docker-compose up -d --build
```

<http://localhost:6002/>


<br>

## Configurations

### Add Migrations
```
cd ./src/Todo.Api
dotnet ef migrations add initial -p ../Todo.Infrastructure/Todo.Infrastructure.csproj -o Database/Migrations
```