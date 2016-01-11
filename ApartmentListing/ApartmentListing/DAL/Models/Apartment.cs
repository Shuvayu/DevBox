using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApartmentListing.DAL.Models
{
    public class Apartment
    {
        [Required]
        public long ID { get; set; }

        [DataType(DataType.Text)]
        [StringLength(maximumLength: 200, MinimumLength = 0)]
        public string Address { get; set; }

        [DataType(DataType.Text)]
        [StringLength(maximumLength: 25, MinimumLength = 0)]
        public string City { get; set; }

        [DataType(DataType.Text)]
        [StringLength(maximumLength: 25, MinimumLength = 0)]
        public string Suburb { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public int Rooms { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid number")]
        public double Bathrooms { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public int Carports { get; set; }


    }
}