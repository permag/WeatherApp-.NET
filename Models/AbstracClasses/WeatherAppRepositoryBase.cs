using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp.Models.AbstracClasses
{
    public abstract class WeatherAppRepositoryBase : IWeatherAppRepository
    {
        public abstract void Add(Location location);
        public abstract void Add(Forecast forecast);
        public abstract void Delete(Location location);
        public abstract void Delete(Forecast forecast);

        public List<Location> FindAllLocations()
        {
            return this.QueryLocations().ToList();
        }

        public List<Forecast> FindAllForecasts()
        {
            return this.QueryForecasts().ToList();
        }

        public Location FindLocation(int locationId)
        {
            return this.QueryLocations()
                .SingleOrDefault(l => l.LocationId == locationId);
        }

        public Forecast FindForecast(int forecastId)
        {
            return this.QueryForecasts()
                .SingleOrDefault(f => f.ForecastId == forecastId);
        }

        public abstract IQueryable<Location> QueryLocations();
        public abstract IQueryable<Forecast> QueryForecasts();
        public abstract void Save();
    }
}