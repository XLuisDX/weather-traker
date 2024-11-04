# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy .csproj files and restore dependencies
COPY src/WeatherTraker.API/WeatherTraker.API.csproj WeatherTraker.API/
COPY src/WeatherTracker.Core/WeatherTracker.Core.csproj WeatherTracker.Core/
COPY src/WeatherTracker.Infrastructure/WeatherTracker.Infrastructure.csproj WeatherTracker.Infrastructure/
COPY src/WeatherTracker.Shared/WeatherTracker.Shared.csproj WeatherTracker.Shared/
RUN dotnet restore "WeatherTraker.API/WeatherTraker.API.csproj"

# Copy the rest of the files and build
COPY src/ ./
WORKDIR "/src/WeatherTraker.API"
RUN dotnet build "WeatherTraker.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "WeatherTraker.API.csproj" -c Release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherTraker.API.dll"]
