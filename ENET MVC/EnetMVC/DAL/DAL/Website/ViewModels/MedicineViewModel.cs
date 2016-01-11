using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Website.ViewModels
{
    /// <summary>
    ///     Model Medicine
    /// </summary>
    public class MedicineViewModel
    {
        public int MedicineId { get; set; }
        [Required]
        public string MedicineName { get; set; }
        [Required]
        public string Description { get; set; }
      
        public string Expiry { get; set; }
        [Required]
        public int ShelfLife { get; set; }
        [Required]
        public double Value { get; set; }
        public bool IstempSensitve { get; set; }

       


    }
}