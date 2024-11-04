using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WeatherTracker.Core.Interfaces.Services;

namespace WeatherTracker.Infrastructure.External.WeatherService
{
    public class OpenWeatherMapResponse
    {
        [JsonProperty("name")]
        public string CityName { get; set; }

        [JsonProperty("sys")]
        public Sys Sys { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }

        [JsonProperty("dt")]
        public long Dt { get; set; }
    }

    public class Main
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    public class Sys
    {
        [JsonProperty("country")]
        public string Country { get; set; }
    }

    public class Weather
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }

    // WeatherTracker.Infrastructure/External/WeatherService/OpenWeatherMapForecastResponse.cs
    public class OpenWeatherMapForecastResponse
    {
        [JsonProperty("list")]
        public ForecastItem[] List { get; set; }

        [JsonProperty("city")]
        public City City { get; set; }
    }

    public class ForecastItem
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }
    }

    public class City
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }

    // WeatherTracker.Infrastructure/External/WeatherService/OpenWeatherMapService.cs
    public class OpenWeatherMapService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OpenWeatherMapService> _logger;
        private readonly WeatherServiceConfig _config;
        private readonly IWeatherDataCacheService _cacheService;

        public OpenWeatherMapService(
            HttpClient httpClient,
            IOptions<WeatherServiceConfig> config,
            ILogger<OpenWeatherMapService> logger,
            IWeatherDataCacheService cacheService)
        {
            _httpClient = httpClient;
            _config = config.Value;
            _logger = logger;
            _cacheService = cacheService;

            _httpClient.BaseAddress = new Uri(_config.BaseUrl);
        }

        public async Task<WeatherDataDto> GetWeatherByLocationAsync(string city, string countryCode)
        {
            return await _cacheService.GetOrSetWeatherDataAsync(
                city,
                countryCode,
                async () =>
                {
                    var response = await _httpClient.GetAsync(
                        $"/data/2.5/weather?q={city},{countryCode}&appid={_config.ApiKey}&units=metric");

                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonConvert.DeserializeObject<OpenWeatherMapResponse>(content);

                    return MapToWeatherDataDto(weatherData);
                });
        }

        public async Task<IEnumerable<WeatherDataDto>> GetWeatherForecastAsync(string city, string countryCode, int days = 5)
        {
            // OpenWeatherMap free API provides 5-day forecast with 3-hour intervals
            var response = await _httpClient.GetAsync(
                $"/data/2.5/forecast?q={city},{countryCode}&appid={_config.ApiKey}&units=metric&cnt={days * 8}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var forecastData = JsonConvert.DeserializeObject<OpenWeatherMapForecastResponse>(content);

            return MapToWeatherForecastDto(forecastData);
        }

        private WeatherDataDto MapToWeatherDataDto(OpenWeatherMapResponse response)
        {
            return new WeatherDataDto
            {
                City = response.CityName,
                Country = response.Sys.Country,
                Temperature = response.Main.Temperature,
                FeelsLike = response.Main.FeelsLike,
                Humidity = response.Main.Humidity,
                Description = response.Weather.FirstOrDefault()?.Description ?? "No description available",
                Timestamp = DateTimeOffset.FromUnixTimeSeconds(response.Dt).DateTime
            };
        }

        private IEnumerable<WeatherDataDto> MapToWeatherForecastDto(OpenWeatherMapForecastResponse response)
        {
            return response.List.Select(item => new WeatherDataDto
            {
                City = response.City.Name,
                Country = response.City.Country,
                Temperature = item.Main.Temperature,
                FeelsLike = item.Main.FeelsLike,
                Humidity = item.Main.Humidity,
                Description = item.Weather.FirstOrDefault()?.Description ?? "No description available",
                Timestamp = DateTimeOffset.FromUnixTimeSeconds(item.Dt).DateTime
            });
        }
    }
}
