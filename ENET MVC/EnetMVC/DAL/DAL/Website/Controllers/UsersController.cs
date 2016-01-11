using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DAL.Models;
using Domain.Contracts;
using Microsoft.AspNet.Identity.EntityFramework;
using Website.ViewModels;

namespace Website.Controllers
{
    public class UsersController : Controller
    {

        public UserContracts userContracts;
        public DistributionCentersContracts dcContracts;
        public UsersController()
        {
            userContracts = new UserContracts();
            dcContracts = new DistributionCentersContracts();
        }
        // GET: Users
        public ActionResult Index()
        {
            AutoMapper.Mapper.CreateMap<User, UserViewModel>();
            var userList = Mapper.Map<IEnumerable<UserViewModel>>(userContracts.GetAll());
            return View(userList);
        }

        public ActionResult Create()
        {
            var dcContracts = new DistributionCentersContracts();
        
            ViewBag.DistributionCenterId = new SelectList(dcContracts.GetAll(), "DistributionCenterId", "Name");
            ViewBag.RoleId = new SelectList(userContracts.GetAllRoles(),"RoleId","RoleName");

           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                AutoMapper.Mapper.CreateMap<UserViewModel, DAL.Models.User>();
                var userModel = AutoMapper.Mapper.Map<User>(user);
                int id =userContracts.Add(userModel);
                if (id <= 0)
                {
                    TempData["fail"] = "Failed to create the user";
                    return RedirectToAction("Create");
                }
                else
                {
                    TempData["success"] = "User Created";
                    AccountController account = new AccountController();
                    return RedirectToAction("Create");
                  
                }
                
            }
            TempData["fail"] = "Failed to create the user";
            return View();
        }

        public ActionResult CreateDistributionCenter()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDistributionCenter(DistributionCenterViewModel dcModel)
        {
            if (ModelState.IsValid)
            {
                AutoMapper.Mapper.CreateMap<DistributionCenterViewModel, DistributionCenter>();
                var dc = Mapper.Map<DistributionCenter>(dcModel);
                dcContracts.Add(dc);
                TempData["success"] = "DC Created";
                return RedirectToAction("CreateDistributionCenter");
            }
            TempData["fail"] = "Failed to create the DC";
           return View();
        }

        public ActionResult ListOfDCs()
        {
            AutoMapper.Mapper.CreateMap<DistributionCenter, DistributionCenterViewModel>();
            var dcs = Mapper.Map<IEnumerable<DistributionCenterViewModel>>(dcContracts.GetAll());
            return View(dcs);
        }

        public ActionResult Delete(int id)
        {

            AutoMapper.Mapper.CreateMap<User, UserViewModel>();
            var user = Mapper.Map<UserViewModel>(userContracts.Get(id));
            
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            userContracts.Delete(id);
            return RedirectToAction("Index");
        }


        public ActionResult DeleteDC(int id)
        {

            AutoMapper.Mapper.CreateMap<DistributionCenter, DistributionCenterViewModel>();
            var dc = Mapper.Map<DistributionCenterViewModel>(dcContracts.Get(id));

            return View(dc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDCConfirm(int id)
        {
            dcContracts.Delete(id);
            return RedirectToAction("ListOfDCs");
        }

    }
}