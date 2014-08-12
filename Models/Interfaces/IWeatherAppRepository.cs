using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public interface IWeatherAppRepository
    {
        void Add(Location location);
        void Add(Forecast forecast);
        void Delete(Location location);
        void Delete(Forecast forecast);
        List<Location> FindAllLocations();
        List<Forecast> FindAllForecasts();
        Location FindLocation(int locationId);
        Forecast FindForecast(int forecastId);
        IQueryable<Location> QueryLocations();
        IQueryable<Forecast> QueryForecasts();
        void Save();
    }
}
