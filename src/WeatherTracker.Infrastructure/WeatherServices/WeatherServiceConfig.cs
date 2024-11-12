namespace WeatherTracker.Infrastructure.External.WeatherService
{
    public class WeatherServiceConfig
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public int CacheExpirationMinutes { get; set; }
    }
}
