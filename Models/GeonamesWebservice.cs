using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace WeatherApp.Models
{
    public class GeonamesWebservice
    {
        public List<Location> FindLocations(string cityName)
        {
            List<Location> locations = new List<Location>();
            // find all geonameId's
            var geoIds = this.FindGeonameIds(cityName);

            // use geonameId's to get locations
            foreach (var id in geoIds)
            {
                locations.Add(this.FindLocation(id.GeonameId));
            }
            return locations;
        }

        private List<GeonamesIdentifier> FindGeonameIds(string cityName)
        {
            var requestUriString =
                String.Format("http://api.geonames.org/search?name={0}&username=killingfloor", cityName);

            var document = LoadDocument(requestUriString);

            return document.Descendants("geoname")
                .Select(geoname => LocationFactory.Create(geoname))
                .ToList();
                
        }

        private Location FindLocation(string geonameId)
        {
            var requestUriString =
                String.Format("http://api.geonames.org/get?geonameId={0}&username=killingfloor", geonameId);

            var document = LoadDocument(requestUriString);

            return document.Descendants("geoname")
                .Select(geoname => LocationDetailsFactory.Create(geoname))
                .SingleOrDefault();
        }

        private XDocument LoadDocument(string requestUriString)
        {
            // Initialize a new WebRequest instance for the GeoNames Search Webservice.
            var request = WebRequest.Create(requestUriString);

            // Get the response from the web service.
            using (var response = request.GetResponse())
            {
                // Parse the response.
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    // Load the response into an XML document.
                    return XDocument.Load(stream);
                }
            }
        }

    }
}