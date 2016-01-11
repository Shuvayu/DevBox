using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Website;
using Website.Controllers;
using DAL.Models;
using Domain;
using Website.ViewModels;
using Domain.Contracts;
using AutoMapper;
namespace Website.Tests.Controllers
{
    [TestClass]
    public class StockControllerTests
    {
        StockController stockController = new StockController();
        PackagesContracts packageContracts = new PackagesContracts();
        
        [TestMethod]
        public void DiscardPackageTest()
        {
          
            var package = packageContracts.GetAll().FirstOrDefault();
            packageContracts.UpdateStatus(package.BarcodeId, 4);
        }
    }
}
