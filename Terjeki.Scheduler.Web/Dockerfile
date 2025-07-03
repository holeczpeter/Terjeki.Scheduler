FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Másold be csak a WebAssembly-projekt csproj-ját, és restore-olj
COPY Terjeki.Scheduler.Web/*.csproj Terjeki.Scheduler.Web/
RUN dotnet restore Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj

# Másold be a teljes WebAssembly kódot
COPY Terjeki.Scheduler.Web/. Terjeki.Scheduler.Web/

WORKDIR /src/Terjeki.Scheduler.Web
# Publish statikus wwwroot fájlokként
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# 2) Runtime lépés: Nginx, statikus fájlok kiszolgálására
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80

COPY --from=build /app/Terjeki.Scheduler.Web/out ./
ENTRYPOINT ["dotnet", "Terjeki.Scheduler.Web.dll"]