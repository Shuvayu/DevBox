using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DAL.Models
{
    /// <summary>
    ///     Package Transaction
    /// </summary>
    public class PackageTransactions
    {
        public int PackageTransactionsId { get; set; }

       
        public int PackageId { get; set; }

        public virtual Package Package { get; set; }
        public string BarcodeId { get; set; }
        public int FromLocId { get; set; }
        public int ToLocId { get; set; }
        [Column("SentBy")]
        public virtual User SentBy { get; set; }
        public DateTime SentOn { get; set; }
       
        [Column("ReceivedBy")]
        public virtual User ReceivedBy{ get; set; }
        public DateTime? ReceivedOn { get; set; }
    }
}