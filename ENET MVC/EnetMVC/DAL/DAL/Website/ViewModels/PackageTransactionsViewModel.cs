using System;

namespace Website.ViewModels
{
    /// <summary>
    ///     Package Transaction
    /// </summary>
    public class PackageTransactionsViewModel
    {
        public int PackageTransactionsId { get; set; }

       
        public int PackageId { get; set; }

        public virtual PackageViewModel Package { get; set; }
        public string BarcodeId { get; set; }
        public int FromLocId { get; set; }
        public int ToLocId { get; set; }
        public string SentBy { get; set; }
        public DateTime SentOn { get; set; }
        public string ReceivedBy { get; set; }
        public DateTime ReceivedOn { get; set; }
    }
}