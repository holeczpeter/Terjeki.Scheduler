FROM nginx AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj", "Terjeki.Scheduler.Web/"]
RUN dotnet restore "./Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj"

COPY . .
WORKDIR "/src/Terjeki.Scheduler.Web"
RUN dotnet build "./Terjeki.Scheduler.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Terjeki.Scheduler.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM  base AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY  Terjeki.Scheduler.Web/nginx.conf /etc/nginx/nginx.conf