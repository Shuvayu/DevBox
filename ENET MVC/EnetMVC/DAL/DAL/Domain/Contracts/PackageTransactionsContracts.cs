using System.Collections.Generic;
using DAL.Models;
using DAL.Services;

namespace Domain.Contracts
{
    public class PackageTransactionsContracts 
    {
        public PackageTransactions PackageTransactionModel { get; set; }
        public PackageTransactionsServices PackageTransactionsServices { get; set; }

        public PackageTransactionsContracts()
        {
            PackageTransactionModel = new PackageTransactions();
            PackageTransactionsServices = new PackageTransactionsServices();
        }

        public int Add(Package package,string username)
        {
          return  PackageTransactionsServices.AddTransaction(package,username);
          
        }

        public int Delete(PackageTransactions PackageTransactions)
        {
            PackageTransactionsServices.Delete(PackageTransactions);
            return PackageTransactions.PackageTransactionsId;
        }

        public ICollection<PackageTransactions> GetAll()
        {
            return PackageTransactionsServices.PackageTransactionses();
        }

        public PackageTransactions Get(int id)
        {
            return PackageTransactionsServices.Details(id);
        }

        public int Update(PackageTransactions PackageTransactions)
        {
            PackageTransactionModel = (PackageTransactions)PackageTransactions;
            PackageTransactionsServices.Update(PackageTransactionModel);
            return PackageTransactionModel.PackageTransactionsId;
        }

        public int ReceivePackage(Package package, string username)
        {
          return  PackageTransactionsServices.ReceivePackages(package, username);
        }
    }
}
