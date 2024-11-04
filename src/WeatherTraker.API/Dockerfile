# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore dependencies
COPY ["WeatherTraker.API/WeatherTraker.API.csproj", "WeatherTraker.API/"]
COPY ["WeatherTracker.Core/WeatherTracker.Core.csproj", "WeatherTracker.Core/"]
COPY ["WeatherTracker.Infrastructure/WeatherTracker.Infrastructure.csproj", "WeatherTracker.Infrastructure/"]
COPY ["WeatherTracker.Shared/WeatherTracker.Shared.csproj", "WeatherTracker.Shared/"]
RUN dotnet restore "WeatherTraker.API/WeatherTraker.API.csproj"

# Copy all files and build
COPY . .
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