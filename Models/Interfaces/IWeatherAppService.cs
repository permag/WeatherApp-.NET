using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public interface IWeatherAppService
    {
        List<Location> FindLocations(string cityName);
        List<Forecast> FindForecast(string country, string county, string city);
    }
}
