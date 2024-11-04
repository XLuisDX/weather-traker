using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTracker.Core.Interfaces.Services
{
    public interface IWeatherDataCacheService
    {
        Task<WeatherDataDto> GetOrSetWeatherDataAsync(string city, string countryCode,
            Func<Task<WeatherDataDto>> getDataFunction);
    }
}
