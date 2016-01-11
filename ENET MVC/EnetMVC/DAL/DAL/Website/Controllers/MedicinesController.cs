using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DAL.Models;
using Domain.Contracts;
using Newtonsoft.Json;
using Website.ViewModels;

namespace Website.Controllers
{
    public class MedicinesController : Controller
    {
        private MedicinesContracts medicineContracts;
        public MedicinesController()
        {
            medicineContracts = new MedicinesContracts();
        }
        // GET: Medicines
        public ActionResult Index()
        {
            AutoMapper.Mapper.CreateMap<Medicine, MedicineViewModel>();
            var medicines = Mapper.Map<IEnumerable<MedicineViewModel>>(medicineContracts.GetAll());
            return View(medicines);
        }


        public ActionResult AddMedicine()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMedicine(MedicineViewModel medicine)
        {
            if (ModelState.IsValid)
            {
                AutoMapper.Mapper.CreateMap<MedicineViewModel, Medicine>();
                var medicineModel = Mapper.Map<Medicine>(medicine);
                medicineContracts.Add(medicineModel);
                TempData["success"] = "Successfully added the medicine";
                return RedirectToAction("AddMedicine");
            }
            TempData["fail"] = "Failed to add the medicine";
            return View();
        }


        public ActionResult GetMedicine(int id)
        {
            var medicine = medicineContracts.Get(id);
            AutoMapper.Mapper.CreateMap<Medicine, MedicineViewModel>();
            AutoMapper.Mapper.CreateMap<Package, PackageViewModel>();
            var medicineVM = Mapper.Map<MedicineViewModel>(medicine);
            medicineVM.Expiry = DateTime.Now.AddDays(medicine.ShelfLife).ToShortDateString();
            return Json(medicineVM);
        }

        public ActionResult Delete(int id)
        {

            AutoMapper.Mapper.CreateMap<Medicine, MedicineViewModel>();
            var medicine = Mapper.Map<MedicineViewModel>(medicineContracts.Get(id));

            return View(medicine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            medicineContracts.Delete(id);
            return RedirectToAction("Index");
        }
    }
}