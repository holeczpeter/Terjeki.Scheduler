# --- Build the Blazor WebAssembly app ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-web
WORKDIR /src

# 1) csak a csproj-ok, gyorsabb cache
COPY Terjeki.Scheduler.Web/*.csproj ./Terjeki.Scheduler.Web/
RUN dotnet restore Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj --verbosity minimal

# 2) az összes forrás
COPY . .

# 3) publish WASM output
WORKDIR /src/Terjeki.Scheduler.Web
RUN dotnet publish -c Release -o /publish /p:UseAppHost=false --verbosity minimal

# --- Runtime image: serve with Nginx ---
FROM nginx:alpine AS runtime-web

# törölheted az alapértelmezett html fájlokat
RUN rm -rf /usr/share/nginx/html/*

# másoljuk át a publishelt wwwroot-ot
COPY --from=build-web /publish/wwwroot /usr/share/nginx/html

# Expose és indítás
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]