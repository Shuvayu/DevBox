using System;
using System.Collections;
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
    public class StockController : Controller
    {
          private PackagesContracts packagesContracts;
        private MedicinesContracts medicineContracts;
        private UserContracts userContracts;
        private DistributionCentersContracts dcContracts;
        private PackageTransactionsContracts packageTransactionsContracts;
        private DistributionsContracts distributionsContracts;
        public IEnumerable<PackageViewModel> CurrentStock { get; set; }

        public StockController()
        {
            packagesContracts = new PackagesContracts();
            medicineContracts = new MedicinesContracts();
            userContracts = new UserContracts();
            dcContracts= new DistributionCentersContracts();
            packageTransactionsContracts = new PackageTransactionsContracts();
            distributionsContracts = new DistributionsContracts();
        }
        // GET: Stock
        public ActionResult Index()
        {
            AutoMapper.Mapper.CreateMap<Package, PackageViewModel>();
            AutoMapper.Mapper.CreateMap<Medicine, MedicineViewModel>();
            AutoMapper.Mapper.CreateMap<PackageStatus, PackageStatusViewModel>();
            AutoMapper.Mapper.CreateMap<PackageTransactions, PackageTransactionsViewModel>();
            var user = User.Identity.GetUserName();
            var packagesModels = packagesContracts.GetAllPackagesAtDC(user);

            var packages = AutoMapper.Mapper.Map<IEnumerable<PackageViewModel>>(packagesModels);
            CurrentStock = packages;
            foreach (var VARIABLE in packages)
            {
                VARIABLE.TransitState =
                    packagesContracts.GetAllStatus().FirstOrDefault(x => x.PackageStatusId == VARIABLE.PackageStatusId).TransitState;
            }
            return View(packages);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult Index(List<PackageViewModel> Models)
        {

            var packagesnotFound = Models.Where(x => x.Found == false);
            foreach (var VARIABLE in packagesnotFound)
            {
                packagesContracts.UpdateStatus(VARIABLE.BarcodeId, 2);
            }
           
            return View(Models);
        }


        public void UpdateCurrentStock()
        {
            AutoMapper.Mapper.CreateMap<Package, PackageViewModel>();
            AutoMapper.Mapper.CreateMap<Medicine, MedicineViewModel>();
            AutoMapper.Mapper.CreateMap<PackageStatus, PackageStatusViewModel>();
            AutoMapper.Mapper.CreateMap<PackageTransactions, PackageTransactionsViewModel>();
            var user = User.Identity.GetUserName();
            var packagesModels = packagesContracts.GetAllPackagesAtDC(user);

            var packages = AutoMapper.Mapper.Map<IEnumerable<PackageViewModel>>(packagesModels);
            CurrentStock = packages;
        }


        public ActionResult Discard()
        {
            
           // AutoMapper.Mapper.CreateMap<Package, PackageViewModel>();
           // var packagedetails = packagesContracts.Get(id);
           //var packagevm= AutoMapper.Mapper.Map<PackageViewModel>(packagedetails);


            return View();
        }

        public ActionResult DiscardConfirm(PackageViewModel pvm)
        {
           var package= packagesContracts.UpdateStatus(pvm.BarcodeId, 4);
            if (package.PackageId > 0)
            {
                TempData["Success"] = "Discarded the package";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["Fail"] = "Failed to Discard the package";
                return View("Index");

            }

        }

        public ActionResult checkPackage(string barcode)
        {
            UpdateCurrentStock();

            ICollection<PackageViewModel> models = (ICollection<PackageViewModel>) CurrentStock;
            foreach (PackageViewModel VARIABLE in models)
            {
                if (VARIABLE.BarcodeId==barcode)
                {
                    VARIABLE.Found = true;
                    return Json("true");
                }
            }

            return View("Index",models);

        }


        public ActionResult Audit(List<PackageViewModel> pvms)
        {
            return RedirectToAction("Index");
        }
      
     
    }
}