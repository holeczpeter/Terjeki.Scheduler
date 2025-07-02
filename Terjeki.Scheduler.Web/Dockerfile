# --- 1. Restore és build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Másoljuk be az egész .sln + minden .csproj-ot, hogy a restore menjen
COPY Terjeki.Scheduler.sln ./
COPY Terjeki.Scheduler.Web/*.csproj Terjeki.Scheduler.Web/
COPY Terjeki.Scheduler.Infrastructure/*.csproj Terjeki.Scheduler.Infrastructure/
COPY Terjeki.Scheduler.Core/*.csproj Terjeki.Scheduler.Core/
COPY Terjeki.Scheduler.Application/*.csproj Terjeki.Scheduler.Application/

# Restore a megoldásra
RUN dotnet restore Terjeki.Scheduler.sln

# Buildeljük a Web projektet (vagy akár az egész solutiont)
WORKDIR /src/Terjeki.Scheduler.Web
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# --- 2. Publish stage ---
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Terjeki.Scheduler.Web
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- 3. Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Ha Blazor WebAssembly-t szolgálsz, és statikus fájlokat akarsz nginx helyett dotnettel:
EXPOSE 80
COPY --from=publish /app/publish ./

ENTRYPOINT ["dotnet", "Terjeki.Scheduler.Web.dll"]