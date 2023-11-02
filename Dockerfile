#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

# Instale as dependÍncias do SQLite
RUN apt-get update && \ apt-get -y --no-install-recommends install default-jre libsqlite3-dev libsqlite3-mod-spatialite && \

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PostBook.csproj", "."]

RUN dotnet tool install --global dotnet-ef --version 6.0.24
RUN dotnet tool install --global dotnet-sonarscanner --version 6.0.24
# RUN dotnet ef database update

RUN dotnet restore "./PostBook.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PostBook.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PostBook.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostBook.dll"]