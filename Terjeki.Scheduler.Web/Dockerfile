# --- 1) Build lépések ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Másoljuk be az egész repót
COPY . .

# Restore a sln alapján
RUN dotnet restore Terjeki.Scheduler.sln

# Build a Web projektet
WORKDIR /src/Terjeki.Scheduler.Web
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# --- 2) Publish ---
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Terjeki.Scheduler.Web
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- 3) Runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "Terjeki.Scheduler.Web.dll"]