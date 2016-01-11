using System.Collections.Generic;

namespace DAL.Models
{
    /// <summary>
    ///     Model Medicine
    /// </summary>
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string Description { get; set; }
        public int ShelfLife { get; set; }
        public double Value { get; set; }
        public bool IstempSensitve { get; set; }

       
    }
}