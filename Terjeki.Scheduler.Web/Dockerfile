# ----------------------------------------
# 1) Build stage: Blazor WASM publish
# ----------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-web
WORKDIR /src

# gyors cache: csak csproj-ok
COPY Terjeki.Scheduler.Web/*.csproj ./Terjeki.Scheduler.Web/
RUN dotnet restore Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj --verbosity minimal

# forráskód másolása
COPY . .

# publishelés
WORKDIR /src/Terjeki.Scheduler.Web
RUN dotnet publish -c Release -o /publish /p:UseAppHost=false --verbosity minimal

# ----------------------------------------
# 2) Runtime stage: statikus fájlok Nginx alatt
# ----------------------------------------
FROM nginx:alpine AS runtime-web

# töröljük a default html-t
RUN rm -rf /usr/share/nginx/html/*

# átmásoljuk a publishelt wwwroot-ot
COPY --from=build-web /publish/wwwroot /usr/share/nginx/html

# port és indítás
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]