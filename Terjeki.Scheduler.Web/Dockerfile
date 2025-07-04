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
WORKDIR /src

# Másoljuk be az összes forráskódot
COPY . .

WORKDIR /src/Terjeki.Scheduler.Web
RUN dotnet build Terjeki.Scheduler.Web.csproj \
    -c Release \
    -o /app/build \
    --no-restore \
    --verbosity minimal

# 3) Publish stage
FROM build AS publish
RUN dotnet publish Terjeki.Scheduler.Web.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    --verbosity minimal \
    -p:UseAppHost=false

# 4) Runtime stage
FROM nginx AS final
EXPOSE 80
EXPOSE 443

WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
# Ha van saját nginx.conf-od:
COPY Terjeki.Scheduler.Web/nginx.conf /etc/nginx/nginx.conf