# --- build stage ---
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Az egész repo-ból másolunk:
COPY . .

# Meglévő Web projektünket publisheljük
WORKDIR /src/Terjeki.Scheduler.Web
RUN dotnet publish -c Release -o /app/publish

# --- runtime stage ---
FROM nginx:alpine AS runtime
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
