namespace WeatherTracker.Core.Interfaces.Services
{
    public interface IWeatherDataCacheService
    {
        Task<WeatherDataDto> GetOrSetWeatherDataAsync(string city, string countryCode,
            Func<Task<WeatherDataDto>> getDataFunction);
    }
}
