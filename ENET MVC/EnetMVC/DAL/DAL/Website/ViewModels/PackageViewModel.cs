using System;
using System.Collections.Generic;

namespace Website.ViewModels
{
    /// <summary>
    ///     Model package
    /// </summary>
    public class PackageViewModel
    {


        public int PackageId { get; set; }
        public virtual ICollection<DistributionViewModel> Distributions { get; set; }

        public virtual MedicineViewModel Medicine { get; set; }
        public int MedicineId { get; set; }
        public string BarcodeId { get; set; }
        public DateTime ExpiryDate { get; set; }
      
     
   
        public DateTime RegisteredOn { get; set; }

        public string TransitState { get; set; }
     
        public int PackageStatusId { get; set; }



        public int  RegisteredBy { get; set; }

        
        public int  RegisteredAt { get; set; }
       
        public int  CurrentLocationId { get; set; }

        public bool Found { get; set; }

        public virtual ICollection<PackageTransactionsViewModel> PackageTransactionses { get; set; }

    }
}