using System;
using System.Collections.Generic;

namespace DAL.Models
{
    /// <summary>
    ///     Model Distribution
    ///     Have the setters for the Distribution DB
    /// </summary>
    public class Distribution
    {
        public int DistributionId { get; set; }

      
        public virtual Package Package { get; set; }
        public int PackageId { get; set; }
        public DateTime On { get; set; }
        public string Description { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }

     

    }
}