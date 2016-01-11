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
    class MedicinesControllerTest
    {
        MedicinesController medicineController = new MedicinesController();
        MedicineViewModel medicineViewModel = new MedicineViewModel();
        MedicinesContracts medicineContracts = new MedicinesContracts();

        [TestMethod]
        public void AddMedicineTest()
        {
            medicineViewModel.MedicineId = 200;
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
            Assert.IsNotNull(medicineController.GetMedicine(medicineViewModel.MedicineId));
        }

        [TestMethod]
        public void UpdateMedicineTest()
        {
            var medicine = Enumerable.First(medicineContracts.GetAll(), x => x.MedicineId == 1);
            medicine.MedicineName = "Crocin";
            medicineContracts.Update(medicine);
        }

        [TestMethod]
        public void MedicineDeleteTest()
        {
            medicineController.DeleteConfirm(1);
        }
    }
}
