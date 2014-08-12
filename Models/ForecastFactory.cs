using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WeatherApp.Models
{
    public class ForecastFactory
    {
        public static Forecast Create(XElement time)
        {
            return new Forecast
            {
                //TimeFrom = time.Attribute("from").Value,
                //TimeTo = DateTime.ParseExact(time.Element("time").Attribute("to").Value,
                //    "ddd MMM dd HH:mm:ss zz00 yyyy",
                //    CultureInfo.InvariantCulture),

                TimePeriod = time.Attribute("period").Value,
                SymbolVar = time.Element("symbol").Attribute("var").Value,
                Temperature = time.Element("temperature").Attribute("value").Value
            };
        }
    }
}