ARG  DOTNET_VERSION=5.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

COPY src/ /app/
RUN dotnet publish /app/Todo.Api/Todo.Api.csproj -c Release -o /public

COPY infra/docker/wait_for_bootstrapping.sh /public

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}
WORKDIR /public
COPY --from=build /public .

# Instala MySql/MariaDb client, útil para o scrip 'wait_for_bootstrapping.sh'
RUN apt-get update && apt-get install -y mariadb-client && apt-get clean -y && rm -rf /var/lib/apt/lists/*

# 'wait_for_bootstrapping' é um arquivo de scrip que segura a linha de comando até que o banco de dados esteja disponivel, 
# e só então passa para o proximo comando: /usr/bin/dotnet
ENTRYPOINT ["./wait_for_bootstrapping.sh", "/usr/bin/dotnet", "/public/Todo.Api.dll"]

