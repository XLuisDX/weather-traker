FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["WeatherTracker.API/*.csproj", "WeatherTracker.API/"]
COPY ["WeatherTracker.Core/*.csproj", "WeatherTracker.Core/"]
COPY ["WeatherTracker.Infrastructure/*.csproj", "WeatherTracker.Infrastructure/"]
COPY ["WeatherTracker.Shared/*.csproj", "WeatherTracker.Shared/"]
RUN dotnet restore "WeatherTracker.API/WeatherTracker.API.csproj"

# Copy the rest of the files and build
COPY . .
RUN dotnet build "WeatherTracker.API/WeatherTracker.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherTracker.API/WeatherTracker.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "WeatherTracker.API.dll"]