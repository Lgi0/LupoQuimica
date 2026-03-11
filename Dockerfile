# 1. Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["LupoQuimica.Api/LupoQuimica.Api.csproj", "LupoQuimica.Api/"]
COPY ["LupoQuimica.Client/LupoQuimica.Client.csproj", "LupoQuimica.Client/"]
COPY ["LupoQuimica.Shared/LupoQuimica.Shared.csproj", "LupoQuimica.Shared/"]

RUN dotnet restore "LupoQuimica.Api/LupoQuimica.Api.csproj"

COPY . .

# Publica a API
WORKDIR "/src/LupoQuimica.Api"
RUN dotnet publish "LupoQuimica.Api.csproj" -c Release -o /app/publish/api

# Publica o Client (Blazor) - Forçamos a saída para uma pasta limpa
WORKDIR "/src/LupoQuimica.Client"
RUN dotnet publish "LupoQuimica.Client.csproj" -c Release -o /app/publish/client

# 2. Final
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copia a API
COPY --from=build /app/publish/api .

# O SEGREDO: Copia o conteúdo da pasta wwwroot que o Blazor gera 
# para a pasta wwwroot que a API serve.
COPY --from=build /app/publish/client/wwwroot ./wwwroot

# Ajuste de permissão (Linux às vezes bloqueia leitura de arquivos estáticos)
USER root
RUN chmod -R 755 ./wwwroot

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "LupoQuimica.Api.dll"]