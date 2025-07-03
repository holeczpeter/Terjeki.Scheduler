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
FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]