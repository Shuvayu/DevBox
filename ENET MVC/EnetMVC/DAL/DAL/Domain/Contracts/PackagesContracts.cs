using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using DAL.Models;
using DAL.Services;

namespace Domain.Contracts
{
    public class PackagesContracts     
    {
        public Package PackageModel { get; set; }
        public Barcode Barcode { get; set; }
        public PackagesServices PackagesServices { get; set; }
        public MedicinesContracts MedicinesContracts { get; set; }
       
       

        public UserContracts UserContracts { get; set; }

        public PackageTransactionsContracts PackagesTransactionContracts { get; set; }

        public PackagesContracts()
        {
            PackageModel = new Package();
            PackagesServices = new PackagesServices();
            MedicinesContracts = new MedicinesContracts();
            PackagesTransactionContracts = new PackageTransactionsContracts();
            UserContracts = new UserContracts();
         }

        public Package Add(Package Packages)
        {
            Barcode = new Barcode();
            PackageModel = (Package)Packages;
            var packageBarcode = Barcode.GenerateBarCodeId();
           // var packageURL = Barcode.Encode(packageBarcode, "~/Images");
            PackageModel.BarcodeId = packageBarcode;
            PackageModel.CurrentLocationId = Packages.RegisteredAt;
            PackagesServices.Add(PackageModel);
            return PackageModel;
        }

        public int Delete(Package Packages)
        {
            PackagesServices.Delete(Packages);
            return Packages.PackageId;
        }

        public ICollection<Package> GetAll()
        {
            return PackagesServices.GetAll();
        }

        public Package Get(string barcodeId)
        {
            return PackagesServices.Details(barcodeId);
        }

        public Package GetInStockInCurrentDC(string barcodeId,int dcId)
        {
            
          var packages=PackagesServices.GetAll().FirstOrDefault(x=>x.BarcodeId.Equals(barcodeId.Trim()) && x.PackageStatus.PackageStatusId==1 && x.CurrentLocation.DistributionCenterId==dcId);
          return packages;
        } 
        public Package GetInTransitInCurrentDC(string barcodeId,int dcId)
        {
            var firstOrDefault = PackagesTransactionContracts.GetAll().FirstOrDefault(x=>x.BarcodeId==barcodeId && x.ToLocId==dcId && x.Package.PackageStatus.PackageStatusId==5);
            if (firstOrDefault != null)
            {
                var package= firstOrDefault.Package;
                return package;
            }
            return new Package();
          
          
        }


        public int Update(Package Packages)
        {
            PackageModel = (Package)Packages;
            PackagesServices.Update(PackageModel);
            return PackageModel.PackageId;
        }

        public ICollection<PackageStatus> GetAllStatus()
        {
            return PackagesServices.GetAllStatus();
        }

        public int SendPackage(Package package,string username)
        {
            PackageTransactionsContracts packageTransactionsContracts = new PackageTransactionsContracts();
           return packageTransactionsContracts.Add(package,username);
            
        }

        public int ReceivePackage(Package package,string username)
        {
            
           return PackagesTransactionContracts.ReceivePackage(package,username);
           


        }
        public ICollection<Package> GetAllPackagesAtDC(string user)
        {
            var userDetails =UserContracts.GetcurrentUserDetails(user);
           return GetAll().Where(x => x.CurrentLocation.DistributionCenterId == userDetails.DistributionCenter.DistributionCenterId && x.PackageStatus.PackageStatusId == 1).ToList();

        }

        public Package UpdateStatus(string barcode,int statusId)
        {

            return PackagesServices.UpdateStatus(barcode, statusId);
        }

    }
}
