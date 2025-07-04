# 1) Restore stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
WORKDIR /src

# Másoljuk be csak a csproj-okat mind a négy projthez:
COPY Terjeki.Scheduler.Core/Terjeki.Scheduler.Core.csproj    Terjeki.Scheduler.Core/
COPY Terjeki.Scheduler.Infrastucure/Terjeki.Scheduler.Infrastucure.csproj Terjeki.Scheduler.Infrastucure/
COPY Terjeki.Scheduler.Application/Terjeki.Scheduler.Application.csproj   Terjeki.Scheduler.Application/
COPY Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj         Terjeki.Scheduler.Web/

# Futtassuk a restore-t a Web csproj-on, ami így visszaállítja a hivatkozott projektek függõségeit is
RUN dotnet restore Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj --verbosity minimal

# 2) Build stage
FROM restore AS build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-web
WORKDIR /src

# Copy project file and restore
COPY Terjeki.Scheduler.Web/*.csproj ./Terjeki.Scheduler.Web/
RUN dotnet restore Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj

# Copy everything and publish static assets
COPY . .
WORKDIR /src/Terjeki.Scheduler.Web
RUN dotnet publish -c Release -o /publish /p:UseAppHost=false

# 2. NGINX stage
FROM nginx:alpine AS web
WORKDIR /usr/share/nginx/html
EXPOSE 80

# Remove default nginx static assets
RUN rm -rf ./*

# Copy Blazor WebAssembly build output
COPY --from=build-web /publish ./

# Start nginx
CMD ["nginx", "-g", "daemon off;"]