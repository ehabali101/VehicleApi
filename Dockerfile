#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["VehicleApi/VehicleApi.csproj", "VehicleApi/"]
RUN dotnet restore "VehicleApi/VehicleApi.csproj"
COPY . .
WORKDIR "/src/VehicleApi"
RUN dotnet build "VehicleApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VehicleApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5000 DOTNET_RUNNING_IN_CONTAINER=true
ENTRYPOINT ["dotnet", "VehicleApi.dll"]