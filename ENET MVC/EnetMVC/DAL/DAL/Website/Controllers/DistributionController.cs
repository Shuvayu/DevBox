using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using Domain.Contracts;
using Microsoft.AspNet.Identity;
using Website.ViewModels;

namespace Website.Controllers
{
    public class DistributionController : Controller
    {
        private DistributionsContracts DistributionsContracts;

        private PackagesContracts packagesContracts;
        private MedicinesContracts medicineContracts;
        private UserContracts userContracts;
        private DistributionCentersContracts dcContracts;
        private PackageTransactionsContracts packageTransactionsContracts;
        private DistributionsContracts distributionsContracts;
        public DistributionController()
        {
            packagesContracts = new PackagesContracts();
            medicineContracts = new MedicinesContracts();
            userContracts = new UserContracts();
            dcContracts = new DistributionCentersContracts();
            packageTransactionsContracts = new PackageTransactionsContracts();
            distributionsContracts = new DistributionsContracts();
        }



        // GET: Distribution
        public ActionResult Index()
        {
            AutoMapper.Mapper.CreateMap<Distribution, DistributionViewModel>();
            var alldistributions = AutoMapper.Mapper.Map<IEnumerable<DistributionViewModel>>(DistributionsContracts.GetAll());

            return View(alldistributions);
        }

        public ActionResult Create()
        {
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistributionViewModel distributionViewModel)
        {
            return View();
        }

        public ActionResult Distribute()
        {
            
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DistributePackage(DistributionViewModel distributionViewModel)
        {
            AutoMapper.Mapper.CreateMap<DistributionViewModel, Distribution>();
            var distribute=  AutoMapper.Mapper.Map<Distribution>(distributionViewModel);
            
            var id= distributionsContracts.Add(distribute,User.Identity.GetUserName(),distributionViewModel.BarcodeId);
            if (id>0)
            {
                TempData["Success"] = "Distributed packages";
                return RedirectToAction("DistributePackage");
            }
            else
            {
                TempData["Fail"] = "Failed to send";
                return View("Distribute");
            }

        }

   

    }
}