# --- 1. Build stage: .NET SDK image a publish-hez ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Másoljuk be a teljes repo-t, hogy a projekteket is lássa a restore/publish
COPY . .

# Lépjünk be a Blazor WebAssembly-projektbe
WORKDIR /src/Terjeki.Scheduler.Web

# Restore és publish (statikus fájlok a /app/publish mappába)
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# --- 2. Runtime stage: NGINX image a statikus fájlokhoz ---
FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html

# Másoljuk át a publish-elt statikus fájlokat (index.html, css, js, stb.)
COPY --from=build /app/publish .    

# Port dokumentációs célból
EXPOSE 80

# Indítsuk az NGINX-et
CMD ["nginx", "-g", "daemon off;"]
