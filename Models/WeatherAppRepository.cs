using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WeatherApp.Models.AbstracClasses;

namespace WeatherApp.Models
{
    public class WeatherAppRepository : WeatherAppRepositoryBase
    {
        private WeatherApp_Entities _entities = new WeatherApp_Entities();

        public override void Add(Location location)
        {
            this._entities.Locations.AddObject(location);
        }

        public override void Add(Forecast forecast)
        {
            this._entities.Forecasts.AddObject(forecast);
        }

        public override void Delete(Location location)
        {
            if (location.EntityState == EntityState.Detached)
            {
                this._entities.Locations.Attach(location);
            }
            this._entities.Locations.DeleteObject(location);
        }

        public override void Delete(Forecast forecast)
        {
            if (forecast.EntityState == EntityState.Detached)
            {
                this._entities.Forecasts.Attach(forecast);
            }
            this._entities.Forecasts.DeleteObject(forecast);
        }

        public override IQueryable<Location> QueryLocations()
        {
            return this._entities.Locations;
        }

        public override IQueryable<Forecast> QueryForecasts()
        {
            return this._entities.Forecasts;
        }

        public override void Save()
        {
            this._entities.SaveChanges();
        }
    }
}