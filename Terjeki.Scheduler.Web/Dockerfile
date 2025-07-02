# --- 1) Build stage: SDK kép Blazor publish-hez ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1.1) Másoljuk be csak a csproj-ot (csak ennek a mappának a gyökerébõl)
COPY *.csproj ./

# 1.2) Restore
RUN dotnet restore

# 1.3) Másoljuk be az összes forrást ebbõl a mappából, és publish-eljük
COPY . ./
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# --- 2) Runtime stage: nginx statikus fájlokhoz ---
FROM nginx:alpine AS runtime

# (SPA routinghoz késõbb ide jöhet az nginx.conf, ha kell)
# COPY nginx.conf /etc/nginx/conf.d/default.conf

# A publisholt wwwroot-ot másoljuk az nginx html könyvtárba
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
