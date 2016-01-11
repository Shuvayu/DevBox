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
    public class MedicinesControllerTests
    {
        private MedicinesController medicineController;
        private MedicineViewModel medicineViewModel;
        private MedicinesContracts medicineContracts;

        public MedicinesControllerTests()
        {
            medicineController = new MedicinesController();
            medicineViewModel = new MedicineViewModel();
            medicineContracts = new MedicinesContracts();
        }

        [TestMethod]
        public void AddMedicineTest()
        {
            
            medicineViewModel.MedicineName = "Panadol";
            medicineViewModel.Description = "For Headache";
            medicineViewModel.Expiry = "06/06/2015";
            medicineViewModel.ShelfLife = 30;
            medicineViewModel.Value = 25.00;
            medicineViewModel.IstempSensitve = false;

            medicineController.AddMedicine(medicineViewModel);
        }

        [TestMethod]
        public void GetMedicineTest()
        {
            var medicineFirst = medicineContracts.GetAll().FirstOrDefault();
            Assert.IsNotNull(medicineController.GetMedicine(medicineFirst.MedicineId));
        }

        [TestMethod]
        public void UpdateMedicineTest()
        {
            var medicineFirst = medicineContracts.GetAll().FirstOrDefault();
          
            medicineFirst.MedicineName = "Crocin";
            medicineContracts.Update(medicineFirst);
        }

    }
}
