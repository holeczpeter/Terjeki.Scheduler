# --- build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj", "Terjeki.Scheduler.Web/"]
COPY ["Terjeki.Scheduler.Application/Terjeki.Scheduler.Application.csproj", "Terjeki.Scheduler.Application/"]
COPY ["Terjeki.Scheduler.Core/Terjeki.Scheduler.Core.csproj", "Terjeki.Scheduler.Core/"]
COPY ["Terjeki.Scheduler.Infrastucure/Terjeki.Scheduler.Infrastucure.csproj", "Terjeki.Scheduler.Infrastucure/"]

RUN dotnet restore "./Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj"
COPY . .
WORKDIR "/src/Terjeki.Scheduler.Web"
RUN dotnet build "./Terjeki.Scheduler.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Terjeki.Scheduler.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Terjeki.Scheduler.Web.dll"]