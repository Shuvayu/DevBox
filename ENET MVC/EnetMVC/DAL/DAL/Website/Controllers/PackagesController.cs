using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DAL.Models;
using Domain.Contracts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Website.Models;
using Website.ViewModels;

namespace Website.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PackagesController : Controller
    {
        private PackagesContracts packagesContracts;
        private MedicinesContracts medicineContracts;
        private UserContracts userContracts;
        private DistributionCentersContracts dcContracts;
        private PackageTransactionsContracts packageTransactionsContracts;
        private DistributionsContracts distributionsContracts;
       public PackagesController()
        {
            packagesContracts = new PackagesContracts();
            medicineContracts = new MedicinesContracts();
            userContracts = new UserContracts();
            dcContracts= new DistributionCentersContracts();
            packageTransactionsContracts = new PackageTransactionsContracts();
            distributionsContracts = new DistributionsContracts();
        }
        // GET: Packages
        public ActionResult Index()
        {
            AutoMapper.Mapper.CreateMap<Package, PackageViewModel>();
            AutoMapper.Mapper.CreateMap<Medicine, MedicineViewModel>();
            AutoMapper.Mapper.CreateMap<PackageStatus, PackageStatusViewModel>();
            AutoMapper.Mapper.CreateMap<PackageTransactions, PackageTransactionsViewModel>();
            var user = User.Identity.GetUserName();
            var packagesModels = packagesContracts.GetAllPackagesAtDC(user);

            var packages = AutoMapper.Mapper.Map<IEnumerable<PackageViewModel>>(packagesModels);
            foreach (var VARIABLE in packages)
            {
                VARIABLE.TransitState =
                    packagesContracts.GetAllStatus().First(x => x.PackageStatusId == VARIABLE.PackageStatusId).TransitState;
            }
            return View(packages);
        }


        public ActionResult CreatePackage()
        {
            
            ViewBag.MedicineId = new SelectList(medicineContracts.GetAll(), "MedicineId", "MedicineName");
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreatePackage(PackageViewModel packageViewModel)
        {
            AutoMapper.Mapper.CreateMap<PackageViewModel,Package>();
            var packageModel = AutoMapper.Mapper.Map<Package>(packageViewModel);
      
            var userName= User.Identity.GetUserName();
            var enetUser =Enumerable.First(userContracts.GetAll(), x => x.UserName == userName);
           
            packageModel.RegisteredBy = enetUser.UserId;
            packageModel.RegisteredAt = enetUser.DistributionCenterId;
            ViewBag.MedicineId = new SelectList(medicineContracts.GetAll(), "MedicineId", "MedicineName");

           packageModel= packagesContracts.Add(packageModel);
            if (!String.IsNullOrEmpty(packageModel.BarcodeId))
            {

                ViewBag.URL= Barcode.Encode(packageModel.BarcodeId, Server.MapPath("~/Images/"));
                ViewBag.URL = "/Images/" + packageModel.BarcodeId + ".jpg";
                TempData["Success"] = "Package Create : Barcode :" + packageModel.BarcodeId;
                ViewBag.Barcode = packageModel.BarcodeId;
                return View(packageViewModel);
            }
            else
            {
                TempData["Fail"] = "Error Creating the barcode";
               return RedirectToAction("CreatePackage");
            }
            return View();
          
        }

        
        public ActionResult SendPackage()
        {
            LoadViewBags();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SendPackage(string id)
        {
            AutoMapper.Mapper.CreateMap<Package, PackageViewModel>();
            var package= Mapper.Map<PackageViewModel>(packagesContracts.Get(id));
           LoadViewBags();
            return View(package);

        }


        public ActionResult GetPackageByBarcode(string id)
        {
            AutoMapper.Mapper.CreateMap<Package, PackageViewModel>();
            AutoMapper.Mapper.CreateMap<Medicine,MedicineViewModel>();
            AutoMapper.Mapper.CreateMap<PackageStatus,PackageStatusViewModel>();
            AutoMapper.Mapper.CreateMap<PackageTransactions,PackageTransactionsViewModel>();
            var user = User.Identity.GetUserName();
            ;
            var package = Mapper.Map<PackageViewModel>(packagesContracts.GetInStockInCurrentDC(id, userContracts.GetcurrentUserDetails(user).DistributionCenterId));
            if (package==null)
            {
                package= new PackageViewModel();
                LoadViewBags();
                package.PackageId=-1;
                return Json(package);
            }
            else
            {
                var medicine = package.Medicine;
                medicine.Expiry = package.ExpiryDate.ToShortDateString();
                return Json(medicine);    
            }
            
        }

        



        /// <summary>
        /// 
        /// </summary>
        /// <param name="packageTransactions"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendPackageWithModel(PackageViewModel packageViewModel)
        {
            AutoMapper.Mapper.CreateMap<PackageViewModel, Package>();
            AutoMapper.Mapper.CreateMap<PackageStatusViewModel, PackageStatus>();
            AutoMapper.Mapper.CreateMap<MedicineViewModel, Medicine>();
            var package=Mapper.Map<Package>(packageViewModel);
            
         var id=   packagesContracts.SendPackage(package,User.Identity.GetUserName());
            if (id>0)
            {
                TempData["Success"] = "Sent the package";
                return Redirect("SendPackage");
            }
            else
            {
               LoadViewBags();
                TempData["Fail"] = "Failed to send the package";
                return View("SendPackage");
            }


        }



        public ActionResult ReceivePackage()
        {
            return View();

        }  
        public ActionResult GetPackageForTransit(string barCode)
        {

            AutoMapper.Mapper.CreateMap<Package, PackageViewModel>();
            AutoMapper.Mapper.CreateMap<Medicine, MedicineViewModel>();
            AutoMapper.Mapper.CreateMap<PackageStatus, PackageStatusViewModel>();
            AutoMapper.Mapper.CreateMap<PackageTransactions, PackageTransactionsViewModel>();
            var user = User.Identity.GetUserName();
            var packagevm = packagesContracts.GetInTransitInCurrentDC(barCode,
                userContracts.GetcurrentUserDetails(user).DistributionCenterId);
            var package = Mapper.Map<PackageViewModel>(packagevm);
            if (package == null)
            {
                package = new PackageViewModel();
                LoadViewBags();
                package.PackageId = -1;
                return Json(package);
            }
            else
            {
                var medicine = package.Medicine;
                medicine.Expiry = package.ExpiryDate.ToShortDateString();
                return Json(medicine);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReceivePackagewithModel(PackageViewModel packageViewModel)
        {

            var package = packagesContracts.Get(packageViewModel.BarcodeId);
            // Change current loc to this loc
            //Transit state
           var id= packagesContracts.ReceivePackage(package, User.Identity.GetUserName());
            if (id>0)
            {
                TempData["Success"] = "Received Package";
                return RedirectToAction("ReceivePackage");

            }
            else
            {
                TempData["Fail"] = "Failed to receive";
                return View("ReceivePackage");
            }

        }
        public void LoadViewBags()
        {
            var user = userContracts.GetcurrentUserDetails(User.Identity.GetUserName());
            var dcs = dcContracts.GetExceptCurrentDC(user.DistributionCenterId);
            ViewBag.CurrentLocationId = new SelectList(dcs, "DistributionCenterId", "Name");
        }

       

     
    }
}