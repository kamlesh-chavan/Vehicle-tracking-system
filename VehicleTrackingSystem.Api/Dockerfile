#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VehicleTrackingSystem.Api/VehicleTrackingSystem.Api.csproj", "VehicleTrackingSystem.Api/"]
RUN dotnet restore "VehicleTrackingSystem.Api/VehicleTrackingSystem.Api.csproj"
COPY . .
WORKDIR "/src/VehicleTrackingSystem.Api"
RUN dotnet build "VehicleTrackingSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VehicleTrackingSystem.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VehicleTrackingSystem.Api.dll"]