using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public class PackagesControllerTests
    {
        PackagesController packageController = new PackagesController();
        PackageViewModel packageViewModel = new PackageViewModel();
        PackagesContracts packageContracts = new PackagesContracts();
        Package package = new Package();
        PackageTransactionsServices packageTransactionService = new PackageTransactionsServices();
        DistributionsContracts distributionsContracts = new DistributionsContracts();
        PackageTransactionsContracts packageTransactionsContracts = new PackageTransactionsContracts();
        UserContracts userContracts = new UserContracts();

        [TestMethod]
        public void CreatePackageTest()
        {
            package.PackageId = 100;
            package.RegisteredAt = 1;
            package.RegisteredBy = 1;

            packageContracts.Add(package);
        }

        [TestMethod]
        public void GetPackageByBarcodeTest()
        {
            var packageTransaction = packageTransactionsContracts.GetAll().FirstOrDefault();
            var package=packageContracts.GetAll().FirstOrDefault(x=>x.BarcodeId==packageTransaction.BarcodeId);

            Assert.IsNotNull(package);

            
        }

        [TestMethod]
        public void UpdatePackageTest()
        {
            var package = packageContracts.GetAll().FirstOrDefault();
            package.PackageStatusId = 3;
            packageContracts.Update(package);

        }

        [TestMethod]
        public void SendPackageTest()
        {
            var roleAgent = userContracts.GetAllRoles().First(x => x.RoleName == "Agent"); 
            var roleDoctors = userContracts.GetAllRoles().First(x => x.RoleName == "Doctor");
            var roleAdmin = userContracts.GetAllRoles().First(x => x.RoleName == "Admin");
            var package = packageContracts.GetAll().FirstOrDefault();

            var userAgent = userContracts.GetAll().First(x => x.RoleId == roleAgent.RoleId);
            packageContracts.SendPackage(package, userAgent.UserName);
            var userDoctor = userContracts.GetAll().First(x => x.RoleId == roleDoctors.RoleId);
            packageContracts.SendPackage(package,userDoctor.UserName);
            var userAdmin = userContracts.GetAll().First(x => x.RoleId == roleAdmin.RoleId);
            packageContracts .SendPackage(package,userAdmin.UserName);
            
        }


        [TestMethod]
        public void ReceivePackageTest()
        {
            var roleAgent = userContracts.GetAllRoles().First(x => x.RoleName == "Agent"); 
            var roleDoctors = userContracts.GetAllRoles().First(x => x.RoleName == "Doctor");
            var roleAdmin = userContracts.GetAllRoles().First(x => x.RoleName == "Admin");
            var package = packageContracts.GetAll().FirstOrDefault();

            var userAgent = userContracts.GetAll().First(x => x.RoleId == roleAgent.RoleId);
            packageContracts.ReceivePackage(package, userAgent.UserName);
            var userDoctor = userContracts.GetAll().First(x => x.RoleId == roleDoctors.RoleId);
            packageContracts.ReceivePackage(package, userDoctor.UserName);
            var userAdmin = userContracts.GetAll().First(x => x.RoleId == roleAdmin.RoleId);
            packageContracts.ReceivePackage(package, userAdmin.UserName);

        }

        [TestMethod]
        public void GetPackageForTransitTest()
        {
            var package = packageContracts.GetAll().FirstOrDefault();
            packageController.GetPackageForTransit(package.BarcodeId);
          
        }
       

    }
}
