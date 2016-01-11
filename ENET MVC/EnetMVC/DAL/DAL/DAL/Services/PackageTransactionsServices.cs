using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using DAL.Models;

namespace DAL.Services
{
    public class PackageTransactionsServices : BaseService
    {

        public PackageTransactionsServices()
            : base()
        {
        }

        public PackageTransactionsServices(EnetContext context)
            : base(context)
        {
        }


        public virtual int AddTransaction(Package package, string username)
        {


            PackageTransactions packageTransactions = new PackageTransactions();
            //update the package status
            package.PackageStatusId = 5;
            var packageModel = Context.Packages.First(x => x.BarcodeId == package.BarcodeId);
            packageModel.PackageStatusId = 5;
            packageModel.PackageStatus =
            Context.PackageStatuses.First(x => x.PackageStatusId == packageModel.PackageStatusId);
            package.PackageId = packageModel.PackageId;
            packageTransactions.PackageId = package.PackageId;
            //Add the packagetransaction
            var userDetails = Context.Users.First(x => x.UserName == username);
            packageTransactions.SentBy = userDetails;
            packageTransactions.SentOn = DateTime.Now;
            packageTransactions.ToLocId = package.CurrentLocationId;
            packageTransactions.FromLocId = userDetails.DistributionCenterId;
            packageTransactions.Package = Context.Packages.First(x => x.PackageId == packageTransactions.PackageId);
            packageTransactions.BarcodeId = packageTransactions.Package.BarcodeId;

            var id = Context.PackageTransactionses.Add(packageTransactions);
            Context.SaveChanges();
            return id.PackageTransactionsId;

        }

        public virtual int Add(PackageTransactions packageTransactions)
        {



            //  PackageTransactions packageTransactions = new PackageTransactions();
            // packageTransactions.PackageId = package.PackageId;

            return 1;

        }

        public virtual int Delete(PackageTransactions packageTransactions)
        {
            Context.PackageTransactionses.Remove(packageTransactions);
            Context.SaveChanges();
            return packageTransactions.PackageTransactionsId;

        }

        public virtual int Update(PackageTransactions packageTransactions)
        {
            AutoMapper.Mapper.CreateMap<PackageTransactions, PackageTransactions>();
            var packagetr =
                Context.PackageTransactionses.First(
                    x => x.PackageTransactionsId == packageTransactions.PackageTransactionsId);

            var mapped = AutoMapper.Mapper.Map<PackageTransactions>(packagetr);
            Context.SaveChanges();
            return mapped.PackageTransactionsId;
        }

        public virtual PackageTransactions Details(int Id)
        {
            return Context.PackageTransactionses.First(x => x.PackageTransactionsId == Id);

        }

        public virtual ICollection<PackageTransactions> PackageTransactionses()
        {
            return Context.PackageTransactionses.ToList();
        }


        public int ReceivePackages(Package package, string username)
        {
            var user = Context.Users.FirstOrDefault(x => x.UserName == username);
            package = Context.Packages.FirstOrDefault(x => x.PackageId == package.PackageId);
            package.CurrentLocation = user.DistributionCenter;
            package.PackageStatusId = 1;
            package.PackageStatus = Context.PackageStatuses.FirstOrDefault(x => x.PackageStatusId == 1);
            var packageTransaction =
                Context.PackageTransactionses.FirstOrDefault(x => x.PackageId == package.PackageId);
            packageTransaction.ReceivedBy = user;
            packageTransaction.ReceivedOn = DateTime.Now;
            Context.SaveChanges();

            return packageTransaction.PackageTransactionsId;
        }
    }
}
