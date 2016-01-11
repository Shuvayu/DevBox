using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    /// <summary>
    ///     Model package
    /// </summary>
    public class Package
    {


        public int PackageId { get; set; }
     

        public virtual Medicine Medicine { get; set; }
        public int MedicineId { get; set; }
        public string BarcodeId { get; set; }
        public DateTime ExpiryDate { get; set; }



        public DateTime RegisteredOn { get; set; }

        public virtual PackageStatus PackageStatus { get; set; }
        public int? PackageStatusId { get; set; }






       [Column("CurrentLocationId")]
        public virtual DistributionCenter CurrentLocation { get; set; }
        [NotMapped]
        public int CurrentLocationId { get; set; }

        [Column("RegisteredAt")]
        public virtual DistributionCenter RegisteredAtDC { get; set; }

        [NotMapped]
        public int RegisteredAt { get; set; }


        [Column("RegisteredBy")]
        public virtual User RegisteredByUser { get; set; }
        [NotMapped]
        public int RegisteredBy { get; set; }





    }
}