using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WeatherApp.Models
{
    public class RegionFactory
    {

        public static Region Create(XElement geoname)
        {
            return new Region
            {
                City = geoname.Element("geoname").Element("toponymName").Value,
                Country = geoname.Element("geoname").Element("countryName").Value
            };
        }
    }
}