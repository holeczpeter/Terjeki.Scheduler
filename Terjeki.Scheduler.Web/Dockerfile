# 1) Build stage: fordítás + publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Másoljuk be csak a projekt fájlokat, hogy a cache jól mûködjön
COPY Terjeki.Scheduler.Web.csproj ./
RUN dotnet restore ./Terjeki.Scheduler.Web.csproj

# Másoljuk be a teljes kódot, és publish-eljük
COPY . ./
RUN dotnet publish Terjeki.Scheduler.Web.csproj -c Release -o /app/publish

# 2) Runtime stage: NGINX, statikus fájlok kiszolgálása
FROM nginx:alpine AS runtime
# Ha szükséged van custom nginx.conf-re, itt másold be:
# COPY nginx.conf /etc/nginx/conf.d/default.conf

# A publishelt wwwroot-ot másoljuk az nginx html könyvtárba
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

