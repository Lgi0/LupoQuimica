# 1. Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia e restaura
COPY ["LupoQuimica.Api/LupoQuimica.Api.csproj", "LupoQuimica.Api/"]
COPY ["LupoQuimica.Client/LupoQuimica.Client.csproj", "LupoQuimica.Client/"]
COPY ["LupoQuimica.Shared/LupoQuimica.Shared.csproj", "LupoQuimica.Shared/"]
RUN dotnet restore "LupoQuimica.Api/LupoQuimica.Api.csproj"

# Copia tudo e publica
COPY . .
RUN dotnet publish "LupoQuimica.Api/LupoQuimica.Api.csproj" -c Release -o /app/publish

# 2. Final
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copia TUDO o que foi publicado (a API já puxa o Client automaticamente no publish)
COPY --from=build /app/publish .

# Garante que a pasta wwwroot existe e tem permissão
RUN mkdir -p wwwroot && chmod -R 755 wwwroot

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "LupoQuimica.Api.dll"]