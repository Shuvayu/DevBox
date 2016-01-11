using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Services
{
    public class PackagesServices : BaseService
    {
        public PackagesServices()
            : base()
        {
        }
        public PackagesServices(EnetContext context) : base(context) { }


        public virtual int Add(Package packages)
        {
            try
            {





                packages.Medicine = Context.Medicines.First(x => x.MedicineId == packages.MedicineId);
                packages.PackageStatusId = 1;
                packages.RegisteredByUser = Context.Users.First(x => x.UserId == packages.RegisteredBy);
                packages.CurrentLocation = Context.DistributionCenter.First(x=>x.DistributionCenterId==packages.RegisteredAt);
                packages.RegisteredAtDC = packages.CurrentLocation;
                packages.RegisteredOn = DateTime.Now;
                packages.MedicineId = packages.Medicine.MedicineId;
                packages.PackageStatus =
                Context.PackageStatuses.First(x => x.PackageStatusId == packages.PackageStatusId);
                Context.Packages.Add(packages);


                Context.SaveChanges();
            }
            catch (DbEntityValidationException)
            {


            }
            catch (Exception e)
            {
            }

            return packages.PackageId;
        }

        public virtual ICollection<Package> GetAll()
        {
            return Context.Packages.ToList();

        }

        public virtual int Delete(Package Packages)
        {
            Context.Packages.Remove(Packages);
            Context.SaveChanges();
            return Packages.PackageId;
        }


        public virtual int Update(Package packages)
        {
            var package = Context.Packages.FirstOrDefault(x=>x.PackageId==packages.PackageId);
            package.PackageStatus = package.PackageStatus;
            
            Context.SaveChanges();
            return package.PackageId;
        }


        public virtual Package Details(string barcodeId)
        {
            return Context.Packages.FirstOrDefault(x => x.BarcodeId == barcodeId);

        }

        public virtual ICollection<PackageStatus> GetAllStatus()
        {
            return Context.PackageStatuses.ToList();
        }

        public virtual Package UpdateStatus(string barcodeId,int statusId)
        {
            var package = Context.Packages.FirstOrDefault(x => x.BarcodeId == barcodeId);
            package.PackageStatus = Context.PackageStatuses.FirstOrDefault(x => x.PackageStatusId == statusId);
            Context.SaveChanges();
            return package;


        }


    }
}
