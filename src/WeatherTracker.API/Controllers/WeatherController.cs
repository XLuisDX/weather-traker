using Microsoft.AspNetCore.Mvc;
using WeatherTracker.Core.Interfaces.Services;

namespace WeatherTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(
            IWeatherService weatherService,
            ILogger<WeatherController> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        // Root API endpoint
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "online",
                message = "Weather API is running",
                endpoints = new[] {
                    "/api/weather/{city}/{countryCode}",
                    "/api/weather/forecast/{city}/{countryCode}?days=5"
                }
            });
        }

        [HttpGet("{city}/{countryCode}")]
        public async Task<ActionResult<WeatherDataDto>> GetWeather(string city, string countryCode)
        {
            try
            {
                _logger.LogInformation("Getting weather for {City}, {CountryCode}", city, countryCode);
                var result = await _weatherService.GetWeatherByLocationAsync(city, countryCode);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting weather for {City}, {CountryCode}", city, countryCode);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("forecast/{city}/{countryCode}")]
        public async Task<ActionResult<IEnumerable<WeatherDataDto>>> GetForecast(
            string city,
            string countryCode,
            [FromQuery] int days = 5)
        {
            try
            {
                _logger.LogInformation("Getting forecast for {City}, {CountryCode}, days: {days}", city, countryCode, days);
                var result = await _weatherService.GetWeatherForecastAsync(city, countryCode, days);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting forecast for {City}, {CountryCode}", city, countryCode);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}