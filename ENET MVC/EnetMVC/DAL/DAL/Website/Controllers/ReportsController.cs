using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using Domain.Contracts;
using Microsoft.AspNet.Identity;
using Website.ViewModels;
using AutoMapper;

namespace Website.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        public PackagesContracts PackagesContracts { get; set; }
        public UserContracts UserContracts { get; set; }

        public DistributionCentersContracts dcContracts;

        public ReportsController()
        {
            PackagesContracts = new PackagesContracts();

            UserContracts = new UserContracts();
        }
        // GET: Reports
        public ActionResult Index()
        {

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "1", Text = "Distribution Center Losses" });
            list.Add(new SelectListItem { Value = "2", Text = "Distribution Center Stock" });
            list.Add(new SelectListItem { Value = "3", Text = "Doctor Activity" });
            list.Add(new SelectListItem { Value = "4", Text = "Global Stock" });
            list.Add(new SelectListItem { Value = "5", Text = "Value in Transit" });

            ViewBag.SelectedReport = list;


            return View();

        }

        public ActionResult GlobalStock()
        {


            AutoMapper.Mapper.CreateMap<Package, PackageViewModel>();
            AutoMapper.Mapper.CreateMap<Medicine, MedicineViewModel>();
            AutoMapper.Mapper.CreateMap<PackageStatus, PackageStatusViewModel>();
            AutoMapper.Mapper.CreateMap<PackageTransactions, PackageTransactionsViewModel>();
            var packagesModels = PackagesContracts.GetAll().Where(x => x.PackageStatus.PackageStatusId == 1);

            var packages = AutoMapper.Mapper.Map<IEnumerable<PackageViewModel>>(packagesModels);
            foreach (var VARIABLE in packages)
            {
                VARIABLE.TransitState =
                    PackagesContracts.GetAllStatus().First(x => x.PackageStatusId == VARIABLE.PackageStatusId).TransitState;
            }

            return PartialView("_GlobalStock", packages);
        }

        public ActionResult ValueinTransit()
        {
            AutoMapper.Mapper.CreateMap<Package, PackageViewModel>();
            AutoMapper.Mapper.CreateMap<Medicine, MedicineViewModel>();
            AutoMapper.Mapper.CreateMap<PackageStatus, PackageStatusViewModel>();

            var TransactionPackages = PackagesContracts.PackagesTransactionContracts;
            var grouped = TransactionPackages.GetAll().Where(x => x.ReceivedOn == null)
                .GroupBy(x => new { x.FromLocId, x.ToLocId }).
                Select(y => new { FromLocId = y.Key.FromLocId, ToLocId = y.Key.ToLocId, Count = y.Count() });

            var lstValueReport = new List<ValueReportViewModel>();
            ValueReportViewModel objValueReport = new ValueReportViewModel();
            DistributionCentersContracts objDCName = new DistributionCentersContracts();
            int totalCount = 0;
            foreach (var o in grouped)
            {
                objValueReport.FromLocation = objDCName.Get(o.FromLocId).Name;
                objValueReport.ToLocation = objDCName.Get(o.ToLocId).Name;
                objValueReport.Count = o.Count;
                lstValueReport.Add(objValueReport);
                totalCount = totalCount + o.Count;
            }

            ValueReportViewModel objValueReport1 = new ValueReportViewModel();

            objValueReport1.FromLocation = "";
            objValueReport1.ToLocation = "Total number of transit packages :";
            objValueReport1.Count = totalCount;
            lstValueReport.Add(objValueReport1);


            //return View(grouped);
            return PartialView("_ValueinTransit", lstValueReport);
        }

        public ActionResult DoctorActivity()
        {
            AutoMapper.Mapper.CreateMap<Package, PackageViewModel>();
            AutoMapper.Mapper.CreateMap<Medicine, MedicineViewModel>();
            AutoMapper.Mapper.CreateMap<PackageStatus, PackageStatusViewModel>();
            AutoMapper.Mapper.CreateMap<PackageTransactions, PackageTransactionsViewModel>();

            return View();
        }

        public ActionResult DCStock()
        {
            AutoMapper.Mapper.CreateMap<Package, PackageViewModel>();
            AutoMapper.Mapper.CreateMap<Medicine, MedicineViewModel>();
            AutoMapper.Mapper.CreateMap<PackageStatus, PackageStatusViewModel>();
            AutoMapper.Mapper.CreateMap<PackageTransactions, PackageTransactionsViewModel>();

            var packagesModels = PackagesContracts.GetAllPackagesAtDC(User.Identity.GetUserName()).Where(x => x.PackageStatus.PackageStatusId == 1);

            var packages = AutoMapper.Mapper.Map<IEnumerable<PackageViewModel>>(packagesModels);
            foreach (var VARIABLE in packages)
            {
                VARIABLE.TransitState =
                    PackagesContracts.GetAllStatus().First(x => x.PackageStatusId == VARIABLE.PackageStatusId).TransitState;
            }

            return PartialView("_DistributionCenterStock", packages);
        }
        public ActionResult DCLosses()
        {
            AutoMapper.Mapper.CreateMap<Package, PackageViewModel>();
            AutoMapper.Mapper.CreateMap<Medicine, MedicineViewModel>();
            AutoMapper.Mapper.CreateMap<PackageStatus, PackageStatusViewModel>();
            AutoMapper.Mapper.CreateMap<PackageTransactions, PackageTransactionsViewModel>();
            AutoMapper.Mapper.CreateMap<DistributionCenter, DistributionCenterViewModel>();

            var lstDcReport = new List<DCLossReportViewModel>();
            var lstDCLostPerDCReport = new List<DCLossReportViewModel>();
            var lstLostDiscardedPerDCReport = new List<DCLossReportViewModel>();

            DCLossReportViewModel objDC = new DCLossReportViewModel();
            DCLossReportViewModel objDCLostPerDC = new DCLossReportViewModel();
            DCLossReportViewModel objDCLostDiscardedPerDC = new DCLossReportViewModel();

            var packagesLostPerDC = PackagesContracts.GetAll().Where(x => x.PackageStatus.PackageStatusId == 2 || x.PackageStatus.PackageStatusId == 3)
                .GroupBy(x => new { x.CurrentLocation.Name })
                .Select(y => new { DistID = y.Key.Name, TotalPackages = y.Count(), TotalCost = y.Sum(w => w.Medicine.Value) });

            var packagesLostDistrDC = PackagesContracts.GetAll().Where(x => x.PackageStatus.PackageStatusId == 2 || x.PackageStatus.PackageStatusId == 3 || x.PackageStatus.PackageStatusId == 4)
                .GroupBy(x => new { x.CurrentLocation.Name })
                .Select(y => new { DistID = y.Key.Name, TotalPackages = y.Count(), TotalCost = y.Sum(w => w.Medicine.Value) });
            
            foreach (var o in packagesLostPerDC)
            {
                objDCLostPerDC.DistributionCenter = o.DistID.ToString();
                objDCLostPerDC.TotalLostORDiscardedPackages = o.TotalPackages;
                objDCLostPerDC.TotalLostORDiscardedValue = o.TotalCost;
                lstDCLostPerDCReport.Add(objDCLostPerDC);
            }

            foreach (var o in packagesLostDistrDC)
            {
                objDCLostDiscardedPerDC.DistributionCenter = o.DistID.ToString();
                objDCLostDiscardedPerDC.TotalLostORDiscardedORDistributedPackages = o.TotalPackages;
                objDCLostDiscardedPerDC.TotalLostORDiscardedORDIstributedValue = o.TotalCost;
                lstLostDiscardedPerDCReport.Add(objDCLostDiscardedPerDC);
            }

            objDCLostDiscardedPerDC = null; objDCLostPerDC = null;

            int i = 0;
            for (i = 0; i < lstDCLostPerDCReport.Count; i++)
            {
                objDC.DistributionCenter = (lstDCLostPerDCReport[i]).DistributionCenter;
                objDC.TotalLostORDiscardedPackages = (lstDCLostPerDCReport[i]).TotalLostORDiscardedPackages;
                objDC.TotalLostORDiscardedValue = (lstDCLostPerDCReport[i]).TotalLostORDiscardedValue;

                objDC.TotalLostORDiscardedORDistributedPackages = (lstLostDiscardedPerDCReport[i]).TotalLostORDiscardedORDistributedPackages;
                objDC.TotalLostORDiscardedORDIstributedValue = (lstLostDiscardedPerDCReport[i]).TotalLostORDiscardedORDIstributedValue;

                objDC.LossRatio = lstDCLostPerDCReport[i].TotalLostORDiscardedValue / lstLostDiscardedPerDCReport[i].TotalLostORDiscardedORDIstributedValue;

                lstDcReport.Add(objDC);
            }
            
            return PartialView("_DistributionCenterLosses", lstDcReport);
        }

    }
}