# 1) BUILD stádium: Blazor WebAssembly lefordítása
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src/Terjeki.Scheduler.Web

# Csak a csproj másolása, hogy gyorsan restore-oljon
COPY Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj ./
RUN dotnet restore Terjeki.Scheduler.Web.csproj

# Teljes WebAssembly-projekt másolása, publish
COPY Terjeki.Scheduler.Web/. ./
RUN dotnet publish Terjeki.Scheduler.Web.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# 2) RUNTIME stádium: nginx a statikus fájlokhoz
FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html

# Ha SPA “fallback” kell, ide másolj egy saját nginx.conf-et:
# COPY nginx.conf /etc/nginx/conf.d/default.conf

# Másold át a publish-elt wwwroot-ot
COPY --from=build /app/publish/wwwroot .

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
