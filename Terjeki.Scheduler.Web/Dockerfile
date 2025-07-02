# --- build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj", "Terjeki.Scheduler.Web/"]
COPY ["Terjeki.Scheduler.Application/Terjeki.Scheduler.Application.csproj", "Terjeki.Scheduler.Application/"]
COPY ["Terjeki.Scheduler.Core/Terjeki.Scheduler.Core.csproj", "Terjeki.Scheduler.Core/"]
COPY ["Terjeki.Scheduler.Infrastucure/Terjeki.Scheduler.Infrastucure.csproj", "Terjeki.Scheduler.Infrastucure/"]
RUN dotnet restore "./Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj"

COPY . .
RUN dotnet publish -c Release -o /app/publish

# --- runtime stage ---
FROM nginx:alpine AS runtime
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
