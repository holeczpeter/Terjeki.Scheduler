# ----------------------------------------
# 1) Build stage: Blazor WASM publish
# ----------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-web
WORKDIR /src

# gyors cache: csak csproj-ok
COPY Terjeki.Scheduler.Web/*.csproj ./Terjeki.Scheduler.Web/
RUN dotnet restore Terjeki.Scheduler.Web/Terjeki.Scheduler.Web.csproj --verbosity minimal

# forr�sk�d m�sol�sa
COPY . .

# publishel�s
WORKDIR /src/Terjeki.Scheduler.Web
RUN dotnet publish -c Release -o /publish /p:UseAppHost=false --verbosity minimal

# ----------------------------------------
# 2) Runtime stage: statikus f�jlok Nginx alatt
# ----------------------------------------
FROM nginx:alpine AS runtime-web

# t�r�lj�k a default html-t
RUN rm -rf /usr/share/nginx/html/*

# �tm�soljuk a publishelt wwwroot-ot
COPY --from=build-web /publish/wwwroot /usr/share/nginx/html

# port �s ind�t�s
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]