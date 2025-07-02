# --- 1. Build stage: SDK image a publishhez ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Másoljuk be az egész repo-t (csak a Web projekthez kell, de így egyszerûbb)
COPY . .

# Lépjünk be a WebAssembly-projekt mappájába
WORKDIR /src/Terjeki.Scheduler.Web

# Restore + publish (statikus wwwroot készül)
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# --- 2. Runtime stage: NGINX image a statikus fájlokhoz ---
FROM nginx:alpine AS runtime

# Ha SPA routinghoz késõbb kell custom conf, legyen itt az nginx.conf:
# (helyette most a defaultot használja az Alpine NGINX)
# COPY nginx.conf /etc/nginx/conf.d/default.conf

WORKDIR /usr/share/nginx/html

# Másoljuk át a publish-elt fájlokat a wwwroot-ból
COPY --from=build /app/publish/wwwroot .

# Alapértelmezett port
EXPOSE 80

# Indítsuk az NGINX-et
CMD ["nginx", "-g", "daemon off;"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj Terjeki.Scheduler.Web/
RUN dotnet restore Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Terjeki.Scheduler.Web.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Terjeki.Scheduler.Web.dll"]