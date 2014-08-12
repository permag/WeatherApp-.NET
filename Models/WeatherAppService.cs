using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{
    public class WeatherAppService : IWeatherAppService
    {
        private IWeatherAppRepository _repository;

        public WeatherAppService()
            :this(new WeatherAppRepository())
        { 
            // ...
        }

        public WeatherAppService(IWeatherAppRepository repository)
        {
            this._repository = repository;
        }

        public List<Location> FindLocations(string cityName)
        {
            var nextUpdate = DateTime.Now.AddMonths(2);

            // first, try get locations from DB
            var location = this._repository.QueryLocations()
                .Where(l => l.CitySearch == cityName)
                .ToList();

            // if no locations or location in DB is old
            if (location != null && (location.Count() == 0 || location.Any(l => l.NextUpdate < DateTime.Now)))
            {
                location.ForEach(l => this._repository.Delete(l));
                location.Clear();
            }

            // if no location in DB, get new locations and insert into DB
            if (location == null || location.Count() == 0)
            {
                var webService = new GeonamesWebservice();
                location = webService.FindLocations(cityName);
                // also add searchName and nextupdate to its property
                foreach (var l in location)
                {
                    l.CitySearch = cityName;
                    l.NextUpdate = nextUpdate;
                }

                location.ForEach(l => this._repository.Add(l));
                this._repository.Save();
            }

            return location;
        }

        public List<Forecast> FindForecast(string country, string county, string city)
        {
            var nextUpdate = DateTime.Now.AddHours(1);

            // check of location for forecast GET request exists.
            var location = this._repository.QueryLocations()
                .Where(l => l.Country == country)
                .Where(l => l.County == county)
                .Where(l => l.City == city)
                .ToList();

            var locationId = location.FirstOrDefault().LocationId;
            this._repository = new WeatherAppRepository();


          
            // first, try get forecast from DB
            var forecast = this._repository.QueryForecasts()
                .Where(f => f.LocationId == locationId)
                .ToList();

            // if no forecast or forecast in DB is old
            if (forecast != null && (forecast.Count() == 0 || forecast.Any(f => f.NextUpdate < DateTime.Now)))
            {
                forecast.ForEach(f => this._repository.Delete(f));
                forecast.Clear();
            }

            // if no forecast in DB, get new forecast and insert into DB
            if (forecast == null || forecast.Count() == 0)
            {
                var webService = new YrNoWebservice();
                forecast = webService.FindForecast(country, county, city);

                foreach (var f in forecast)
                {
                    f.NextUpdate = nextUpdate;
                    f.LocationId = locationId;

                    this._repository.Add(f);
                }

                //forecast.ForEach(f => this._repository.Add(f));
                this._repository.Save();
            }

            return forecast;
        }
    }
}