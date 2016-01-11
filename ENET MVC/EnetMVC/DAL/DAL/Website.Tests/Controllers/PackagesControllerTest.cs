using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Website;
using Website.Controllers;
using DAL.Models;
using DAL.Services;
using Domain;
using Website.ViewModels;
using Domain.Contracts;
using AutoMapper;

namespace Website.Tests.Controllers
{
    [TestClass]
    class PackagesControllerTest
    {
        PackagesController packageController = new PackagesController();
        PackageViewModel packageViewModel = new PackageViewModel();
        PackagesContracts packageContract = new PackagesContracts();
        Package package = new Package();
        PackageTransactionsServices packageTransactionService = new PackageTransactionsServices();

        [TestMethod]
        public void CreatePackageTest()
        {
            package.PackageId = 100;
            package.RegisteredAt = 1;
            package.RegisteredBy = 1;

            packageContract.Add(package);
        }

        [TestMethod]
        public void GetPackageByBarcodeTest()
        {
            packageController.GetPackageByBarcode("1000000000");
        }

        [TestMethod]
        public void SendPackageTest()
        {
            package.PackageId = 1;
            package.BarcodeId = "1000000000";
            packageTransactionService.AddTransaction(package, "TestUser");
        }

        [TestMethod]
        public void UpdatePackageTest()
        {
            var package = Enumerable.First(packageContract.GetAll(), x => x.PackageId == 1);
            package.PackageId = 500;
            packageContract.Update(package);

        }

        [TestMethod]
        public void DeletePackageTest()
        {
            var package = Enumerable.First(packageContract.GetAll(), x => x.PackageId == 1);
            packageContract.Delete(package);
        }

    }
}
