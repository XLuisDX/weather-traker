using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherTracker.Core.Interfaces.Services;
using WeatherTracker.Infrastructure.External.WeatherService;

public class WeatherDataCacheService : IWeatherDataCacheService
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<WeatherDataCacheService> _logger;
    private readonly WeatherServiceConfig _config;

    public WeatherDataCacheService(
        IMemoryCache cache,
        IOptions<WeatherServiceConfig> config,
        ILogger<WeatherDataCacheService> logger)
    {
        _cache = cache;
        _config = config.Value;
        _logger = logger;
    }

    public async Task<WeatherDataDto> GetOrSetWeatherDataAsync(
        string city,
        string countryCode,
        Func<Task<WeatherDataDto>> getDataFunction)
    {
        string cacheKey = $"weather_{city.ToLower()}_{countryCode.ToLower()}";

#pragma warning disable CS8603 // Possible null reference return.
        return await _cache.GetOrCreateAsync(
            cacheKey,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(_config.CacheExpirationMinutes);

                _logger.LogInformation(
                    "Cache miss for {City}, {CountryCode}. Fetching new data.",
                    city, countryCode);

                return await getDataFunction();
            });
#pragma warning restore CS8603 // Possible null reference return.
    }
}