# 1) Build stage: restore & publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy only the WASM project and its assets
COPY ./*.csproj ./
RUN dotnet restore Terjeki.Scheduler.Web.csproj

# copy the rest of the client sources
COPY . ./

# publish to get static wwwroot
RUN dotnet publish Terjeki.Scheduler.Web.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# 2) Runtime stage: serve with NGINX
FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html

# (optional) if you need client-side fallback routing, you can add your own nginx.conf:
# COPY nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/publish/wwwroot .

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
