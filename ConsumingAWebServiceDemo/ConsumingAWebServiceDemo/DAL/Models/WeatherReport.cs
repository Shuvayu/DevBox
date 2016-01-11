using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsumingAWebServiceDemo.DAL.Models
{
    public class WeatherReport
    {
        public int id { get; set; }

        [Display(Name = "Location")]
        public string location { get; set; }

        [Display(Name = "Time")]
        public string time  { get; set; }

        [Display(Name = "Wind")]
        public string wind { get; set; }

        [Display(Name = "Visibility")]
        public string visibility { get; set; }

        [Display(Name = "Sky Conditions")]
        public string skyConditions { get; set; }

        [Display(Name = "Temparature")]
        public string temperature { get; set; }

        [Display(Name = "Dewpoint")]
        public string dewpoint { get; set; }

        [Display(Name = "Relative Humidity")]
        public string relativeHumidity { get; set; }

        [Display(Name = "Pressure")]
        public string pressure { get; set; }

        [Display(Name = "Status")]
        public string status { get; set; }
    }
}