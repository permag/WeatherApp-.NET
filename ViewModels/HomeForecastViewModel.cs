using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class HomeForecastViewModel
    {
       public List<Forecast> Forecasts { get; set; }
    }
}