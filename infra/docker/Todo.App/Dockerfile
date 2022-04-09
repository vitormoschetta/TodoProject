ARG  DOTNET_VERSION=5.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

COPY src/Todo.App /app/
RUN dotnet publish /app/Todo.App.csproj -c Release -o /public

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine
WORKDIR /public
COPY --from=build /public .

ENTRYPOINT ["/usr/bin/dotnet", "/public/Todo.App.dll"]