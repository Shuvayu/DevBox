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
    class DistributionControllerTest
    {
        DistributionController distributionController = new DistributionController();
        DistributionsContracts distributionContracts = new DistributionsContracts();
        Distribution distribution = new Distribution();

        [TestMethod]
        public void CreateDistributionTest()
        {
            distribution.DistributionId = 200;
            distribution.PackageId = 1;
            distribution.On = new DateTime(2015, 6, 6);
            distribution.Description = "Testing Distribution";
            distribution.UserId = 1;

            distributionContracts.Add(distribution);
        }

        [TestMethod]
        public void UpdateDistributionTest()
        {
            var distribution = Enumerable.First(distributionContracts.GetAll(), x => x.DistributionId == 1);
            distribution.Description = "Updating Testing Distribution";

          //  distributionContracts.Update(distribution);
        }

        [TestMethod]
        public void DeleteDistribution()
        {
            distributionContracts.Delete(1);
        }

    }
}
