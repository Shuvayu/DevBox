using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Services
{
    public class DistributionCentersServices : BaseService
    {

        public DistributionCentersServices()
            : base()
        {
        }

        public DistributionCentersServices(EnetContext context) : base(context) { }




        public virtual ICollection<DistributionCenter> GetAll()
        {
            return Context.DistributionCenter.ToList();

        }

    

        public virtual int Add(DistributionCenter distributionCenter)
        {
            Context.DistributionCenter.Add(distributionCenter);
            Context.SaveChanges();
            return distributionCenter.DistributionCenterId;
        }

        public virtual int Delete(int distributionCenterId)
        {
            var distributionCenter = Context.DistributionCenter.First(x => x.DistributionCenterId == distributionCenterId);
            Context.DistributionCenter.Remove(distributionCenter);
            Context.SaveChanges();
            return distributionCenter.DistributionCenterId;
        }

        public virtual int Update(DistributionCenter distrubutionCenter)
        {
            var curr_distributionCenter = Context.DistributionCenter.First(x => x.DistributionCenterId == distrubutionCenter.DistributionCenterId);
            curr_distributionCenter.PhoneNumber = distrubutionCenter.PhoneNumber;
            curr_distributionCenter.Name = distrubutionCenter.Name;
            curr_distributionCenter.Address = distrubutionCenter.Address;
            Context.SaveChanges();
            return distrubutionCenter.DistributionCenterId;
        }

        public virtual DistributionCenter Details(int distributionCenterId)
        {
            return Context.DistributionCenter.First(x => x.DistributionCenterId == distributionCenterId);
        }
    }
}
