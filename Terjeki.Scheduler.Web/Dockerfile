# 1) BUILD stádium
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# csak a csproj-ot másoljuk be, hogy gyorsan lehessen cache-elni a restore-t
COPY *.csproj ./
RUN dotnet restore

# utána a teljes kód
COPY . ./
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# 2) RUNTIME stádium
FROM nginx:alpine AS runtime
WORKDIR /usr/share/nginx/html

# a Blazor wwwroot statikus fájljai
COPY --from=build /app/publish/wwwroot ./

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
