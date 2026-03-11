# 1. Estágio de Build (SDK)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia os arquivos de projeto (incluindo o Client e Shared que estavam faltando)
COPY ["LupoQuimica.Api/LupoQuimica.Api.csproj", "LupoQuimica.Api/"]
COPY ["LupoQuimica.Client/LupoQuimica.Client.csproj", "LupoQuimica.Client/"]
COPY ["LupoQuimica.Shared/LupoQuimica.Shared.csproj", "LupoQuimica.Shared/"]

# Restaura as dependências de tudo
RUN dotnet restore "LupoQuimica.Api/LupoQuimica.Api.csproj"
RUN dotnet restore "LupoQuimica.Client/LupoQuimica.Client.csproj"

# Copia o código todo
COPY . .

# 2. Publica a API
WORKDIR "/src/LupoQuimica.Api"
RUN dotnet publish "LupoQuimica.Api.csproj" -c Release -o /app/publish/api

# 3. Publica o Client (Blazor)
WORKDIR "/src/LupoQuimica.Client"
RUN dotnet publish "LupoQuimica.Client.csproj" -c Release -o /app/publish/client

# ... (Mantenha o build igual até a parte final) ...

# Estágio Final
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# 1. Copia os binários da API
COPY --from=build /app/publish/api .

# 2. Força a criação da pasta wwwroot e copia o conteúdo do Client
# O segredo está no "*" para pegar o conteúdo de dentro do wwwroot gerado
RUN mkdir -p wwwroot
COPY --from=build /app/publish/client/wwwroot ./wwwroot/

# Verifica se o arquivo existe (Isso vai aparecer no log do Railway se falhar)
RUN ls -la ./wwwroot/_framework/blazor.webassembly.js || echo "ARQUIVO NAO ENCONTRADO NO BUILD"

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "LupoQuimica.Api.dll"]