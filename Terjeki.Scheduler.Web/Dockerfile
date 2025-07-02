# --- 1. Build stage: SDK image a publishhez ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Másoljuk be az egész monorepo-t, hogy a csproj-ok is elérhetõk legyenek
COPY . .

# Lépjünk be a WebAssembly-projekt mappájába
WORKDIR /src/Terjeki.Scheduler.Web

# Restore + publish (statikus wwwroot készül)
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# --- 2. Runtime stage: NGINX image a statikus fájlokhoz ---
FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html

# (Opcionális) Ha SPA routinghoz kell, custom nginx.conf-t másolj be:
# COPY nginx.conf /etc/nginx/conf.d/default.conf

# Másoljuk át a publish-elt fájlokat
COPY --from=build /app/publish/wwwroot .

# Port dokumentációs célból
EXPOSE 80

# Indítsuk az NGINX-et
CMD ["nginx", "-g", "daemon off;"]