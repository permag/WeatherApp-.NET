using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherApp.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "CitySearch")]HomeIndexViewModel model)
        {
            model.Locations = new List<Location>();
            try
            {
                if (ModelState.IsValid)
                {
                    var service = new WeatherAppService();
                    model.Locations = service.FindLocations(model.CitySearch);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View("Index", model);
        }

        public ActionResult Forecast()
        {
            var model = new HomeForecastViewModel();
            model.Forecasts = new List<Forecast>();

            try
            {
                string country = Request.QueryString["country"];
                string county = Request.QueryString["county"];
                string city = Request.QueryString["city"];
                ViewBag.dayNow = (int)System.DateTime.Now.DayOfWeek;

                var sercive = new WeatherAppService();
                model.Forecasts = sercive.FindForecast(country, county, city);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View("Forecast", model);
        }

        public ActionResult Error()
        {
            return View("Error");
        }


    }
}
