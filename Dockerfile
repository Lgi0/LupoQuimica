# Estágio de Build - Usando SDK 10.0
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia os arquivos de projeto
COPY ["LupoQuimica.Api/LupoQuimica.Api.csproj", "LupoQuimica.Api/"]
COPY ["LupoQuimica.Shared/LupoQuimica.Shared.csproj", "LupoQuimica.Shared/"]

RUN dotnet restore "LupoQuimica.Api/LupoQuimica.Api.csproj"

# Copia o restante e compila
COPY . ./
WORKDIR "/src/LupoQuimica.Api"
RUN dotnet build "LupoQuimica.Api.csproj" -c Release -o /app/build

# Publica
FROM build AS publish
RUN dotnet publish "LupoQuimica.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio final (Runtime) - Usando ASP.NET 10.0
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Etapa de publicação da API
FROM build AS publish
RUN dotnet publish "LupoQuimica.Api/LupoQuimica.Api.csproj" -c Release -o /app/publish

# Etapa de publicação do Client (O QUE PODE ESTAR FALTANDO)
FROM build AS publish-client
RUN dotnet publish "LupoQuimica.Client/LupoQuimica.Client.csproj" -c Release -o /app/publish-client

# Estágio final (onde tudo se encontra)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# COPIA OS ARQUIVOS DO FRONT PARA DENTRO DA API
COPY --from=publish-client /app/publish-client/wwwroot ./wwwroot

ENTRYPOINT ["dotnet", "LupoQuimica.Api.dll"]

