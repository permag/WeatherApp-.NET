using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace WeatherApp.Models
{
    public class YrNoWebservice
    {
       public List<Forecast> FindForecast(string country, string county, string city)
       {
            XDocument document;

            var requestUriString =
                String.Format("http://www.yr.no/place/{0}/{1}/{2}/forecast.xml", country, county, city);

            var request = WebRequest.Create(requestUriString);

            using (var respons = request.GetResponse())
            using (var stream = new StreamReader(respons.GetResponseStream()))
            {
                document = XDocument.Load(stream);
            }

            return document.Descendants("time")
                .Select(time => ForecastFactory.Create(time))
                .Take(20)
                .ToList();
       }
    }
}