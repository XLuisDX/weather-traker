namespace WeatherTracker.Core.Interfaces.Services
{
    public interface IWeatherService
    {
        Task<WeatherDataDto> GetWeatherByLocationAsync(string city, string countryCode);
        Task<IEnumerable<WeatherDataDto>> GetWeatherForecastAsync(string city, string countryCode, int days = 5);
    }
}