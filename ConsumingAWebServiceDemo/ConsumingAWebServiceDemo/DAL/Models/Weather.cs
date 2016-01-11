using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsumingAWebServiceDemo.DAL.Models
{
    public class Weather
    {

        public int id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Country")]
        [StringLength(maximumLength: 15, MinimumLength = 2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string country { get; set; }

        [Display(Name = "Cities")]
        public string cities { get; set; }
    }
}