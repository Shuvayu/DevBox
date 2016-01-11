using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.ViewModels
{
    public class DCLossReportViewModel
    {
        public string DistributionCenter { get; set; }

        public int TotalLostORDiscardedPackages { get; set; }

        public int TotalLostORDiscardedORDistributedPackages { get; set; }

        public double TotalLostORDiscardedValue { get; set; }

        public double TotalLostORDiscardedORDIstributedValue { get; set; }

        public double LossRatio { get; set; }
    }
}