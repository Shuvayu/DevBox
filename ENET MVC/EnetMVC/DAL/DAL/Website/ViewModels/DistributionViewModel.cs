using System;
using System.ComponentModel.DataAnnotations;

namespace Website.ViewModels
{
    /// <summary>
    ///     Model Distribution
    ///     Have the setters for the Distribution DB
    /// </summary>
    public class DistributionViewModel
    {
        public int DistributionId { get; set; }

        public string Username { get; set; }
        public int PackageId { get; set; }
        public string BarcodeId { get; set; }
        public DateTime On { get; set; }
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string Description { get; set; }
       
        public int UserId { get; set; }


        public virtual MedicineViewModel Medicine { get; set; }
    }
}