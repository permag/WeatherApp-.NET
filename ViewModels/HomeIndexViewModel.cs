using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class HomeIndexViewModel
    {
        [DisplayName("Location")]
        [Required(ErrorMessage = "Please enter a location.")]
        [StringLength(50, ErrorMessage = "The search field may not contain more than 50 characthers.")]
        public string CitySearch { get; set; }

        public List<Location> Locations { get; set; }
    }
}