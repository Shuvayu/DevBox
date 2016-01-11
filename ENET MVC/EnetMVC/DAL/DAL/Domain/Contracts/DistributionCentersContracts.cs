using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Services;

namespace Domain.Contracts
{
    public class DistributionCentersContracts 
    {
        private DistributionCenter DcModel { get; set; }
        private DistributionCentersServices DcServices { get; set; }

        public DistributionCentersContracts()
        {
            DcModel= new DistributionCenter();
            DcServices = new DistributionCentersServices();
        }
        public int Add(DistributionCenter dc)
        {
            DcModel =  dc;
            DcServices.Add(DcModel);
            return DcModel.DistributionCenterId;
        }

        public int Delete(int id)
        {
            DcServices.Delete(id);
            return DcModel.DistributionCenterId;
        }

     

        public ICollection<DistributionCenter> GetAll()
        {
            return DcServices.GetAll();
        }

        public DistributionCenter Get(int id)
        {
            return DcServices.Details(id);
        }

        public ICollection<DistributionCenter> GetExceptCurrentDC(int currentDC)
        {
            return DcServices.GetAll().Where(x=>x.DistributionCenterId != currentDC).ToList();
        }

        public int Update(DistributionCenter DistributionCenters)
        {
            DcModel = (DistributionCenter)DistributionCenters;
            DcServices.Update(DcModel);
            return DcModel.DistributionCenterId;
        }
    }
}
