using System;
using System.Globalization;
using System.Xml.Linq;

namespace WeatherApp.Models
{
    public class LocationFactory
    {
        public static GeonamesIdentifier Create(XElement geoname)
        {
            return new GeonamesIdentifier
            {
                GeonameId = geoname.Element("geonameId").Value
            };
        }
    }

    public class LocationDetailsFactory
    {
        public static Location Create(XElement geoname)
        {
            return new Location
            {
                Country = geoname.Element("countryName").Value,
                County = geoname.Element("adminName1").Value,
                City = geoname.Element("toponymName").Value
            };
        }
    }
}