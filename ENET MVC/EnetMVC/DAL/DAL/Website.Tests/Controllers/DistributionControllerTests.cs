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
    public class DistributionControllerTests
    {
        DistributionController distributionController = new DistributionController();
        DistributionsContracts distributionContracts = new DistributionsContracts();
        Distribution distribution = new Distribution();
        PackagesContracts packagesContracts = new PackagesContracts();

        [TestMethod]
        public void CreateDistributionTest()
        {
            var package = packagesContracts.GetAll().FirstOrDefault();
            distribution.PackageId = package.PackageId;
            distribution.On = new DateTime(2015, 6, 6);
            distribution.Description = "Testing Distribution";
            distribution.UserId = 1;

            distributionContracts.Add(distribution,"","");
        }

        [TestMethod]
        public void UpdateDistributionTest()
        {
            var dist = distributionContracts.GetAll().FirstOrDefault();
            dist.Description = "Updating Testing Distribution";

            //  distributionContracts.Update(distribution);
        }

        [TestMethod]
        public void DeleteDistribution()
        {
            var dist = distributionContracts.GetAll().FirstOrDefault();

            distributionContracts.Delete(dist.DistributionId);
        }
    }
}
