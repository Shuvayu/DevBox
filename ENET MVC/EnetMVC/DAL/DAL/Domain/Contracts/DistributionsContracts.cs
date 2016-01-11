using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DAL.Models;
using DAL.Services;

namespace Domain.Contracts
{
    public class DistributionsContracts
    {
        public Distribution DistributionModel { get; set; }
        public DistributionsServices DistributionsServices { get; set; }
        public PackagesContracts PackagesContracts { get; set; }
        public UserContracts UserContracts { get; set; }

        public DistributionsContracts()
        {
            DistributionModel = new Distribution();
            DistributionsServices = new DistributionsServices();
            PackagesContracts= new PackagesContracts();
            UserContracts = new UserContracts();
        }
        public int Add(Distribution Distributions,string username,string barcode)
        {
          
           return DistributionsServices.Add(Distributions,username,barcode);
           
        }

        public int Delete(int id)
        {
            DistributionsServices.Delete(id);
            return DistributionModel.DistributionId;
        }

        public ICollection<Distribution> GetAll()
        {
            return DistributionsServices.Distribution;
        }

        public Distribution Get(int id)
        {
            return DistributionsServices.Details(id);
        }

       
        
    }
}
