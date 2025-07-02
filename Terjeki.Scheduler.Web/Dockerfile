# --- 1) Build lépések ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Másoljuk be az egész repót
COPY . .

# Restore a sln alapján
# 1) Build stage: fordítjuk és publish-eljük a Blazor WASM-t
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Másoljuk be a csproj-ot és a kódot
COPY Terjeki.Scheduler.Web.csproj ./
RUN dotnet restore Terjeki.Scheduler.Web.csproj

COPY . ./
RUN dotnet publish Terjeki.Scheduler.Web.csproj -c Release -o /app/publish

# 2) Runtime stage: NGINX-sel szolgáljuk ki a wwwroot-ot
FROM nginx:alpine
# (opcionális: ha SPA routinghoz kell, készíts saját nginx.conf-ot)
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

# Ha külön SPA-handling kell, illeszd ide a custom nginx.conf-t:
# COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
