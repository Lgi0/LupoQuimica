# 1. Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia os projetos
COPY ["LupoQuimica.Api/LupoQuimica.Api.csproj", "LupoQuimica.Api/"]
COPY ["LupoQuimica.Client/LupoQuimica.Client.csproj", "LupoQuimica.Client/"]
COPY ["LupoQuimica.Shared/LupoQuimica.Shared.csproj", "LupoQuimica.Shared/"]

# Restaura
RUN dotnet restore "LupoQuimica.Api/LupoQuimica.Api.csproj"

# Copia tudo e publica APENAS a API
# (O .NET vai notar a referência e publicar o Client junto)
COPY . .
WORKDIR "/src/LupoQuimica.Api"
RUN dotnet publish "LupoQuimica.Api/LupoQuimica.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Estágio Final
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# --- INSTALA A LIB QUE ESTÁ DANDO ERRO NO LOG ---
USER root
RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

# Copia tudo o que foi publicado
COPY --from=build /app/publish .
# Forçamos a verificação da pasta wwwroot
RUN ls -la ./wwwroot

# Garante permissão total na pasta de arquivos estáticos
RUN chmod -R 755 wwwroot

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "LupoQuimica.Api.dll"]