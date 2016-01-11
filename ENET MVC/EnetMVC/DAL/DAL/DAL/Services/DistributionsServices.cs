using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Services
{
    public class DistributionsServices : BaseService
    {
        public DistributionsServices()
            : base()
        {
        }

        public DistributionsServices(EnetContext context) : base(context) { }


        public virtual int Add(Distribution Distribution,string username,string barcode)
        {

            Distribution.User = Context.Users.ToList().FirstOrDefault(x => x.UserName == username);
            var package = Context.Packages.ToList().FirstOrDefault(x => x.BarcodeId == barcode);
            package.PackageStatus = Context.PackageStatuses.FirstOrDefault(x => x.PackageStatusId == 4);
            Distribution.Package = package;
            Distribution.On = DateTime.Now;
           

           
          Context.Distributions.Add(Distribution);

            Context.SaveChanges();
            return Distribution.DistributionId;
        }

        public virtual ICollection<Distribution> Distribution
        {

            get { return Context.Distributions.OrderBy(x => x.DistributionId).ToList(); }
        }

        public virtual int Delete(int DistributionId)
        {
            var Distribution = Context.Distributions.First(x => x.DistributionId == DistributionId);
            Context.Distributions.Remove(Distribution);
            Context.SaveChanges();
            return Distribution.DistributionId;
        }

        public virtual Distribution Details(int DistributionId)
        {
            return Context.Distributions.First(x => x.DistributionId == DistributionId);

        }
    }
}

