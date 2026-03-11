FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia os arquivos de projeto
COPY ["LupoQuimica.Api/LupoQuimica.Api.csproj", "LupoQuimica.Api/"]
COPY ["LupoQuimica.Client/LupoQuimica.Client.csproj", "LupoQuimica.Client/"]
COPY ["LupoQuimica.Shared/LupoQuimica.Shared.csproj", "LupoQuimica.Shared/"]

# Restaura tudo
RUN dotnet restore "LupoQuimica.Api/LupoQuimica.Api.csproj"

# Copia o código e publica
COPY . .
WORKDIR "/src/LupoQuimica.Api"
RUN dotnet publish "LupoQuimica.Api.csproj" -c Release -o /app/publish

# Estágio Final
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Força a criação da pasta e permissão (previne 404 por falta de acesso)
USER root
RUN chmod -R 755 wwwroot

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "LupoQuimica.Api.dll"]